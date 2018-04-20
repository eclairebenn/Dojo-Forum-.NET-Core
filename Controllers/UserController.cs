using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using log_reg_identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace log_reg_identity.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private IdentityContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(
            IdentityContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        [Route("")]

        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            return View("Register");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User NewUser = new User { UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(NewUser, model.Password);

                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(NewUser, "Basic");
                    await _signInManager.SignInAsync(NewUser, isPersistent: false);
                    return RedirectToAction("Index", "Forum");
                }

                foreach( var error in result.Errors )
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [Route("login")]

        public async Task<IActionResult> DisplayLogin()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }

        [HttpPost]
        [Route("user/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                User returnUser = await _userManager.FindByEmailAsync(model.Email);      
                if(returnUser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(returnUser, model.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Forum");
                    }
                }            
            }
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}
