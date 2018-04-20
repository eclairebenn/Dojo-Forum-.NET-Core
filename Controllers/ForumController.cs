using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using log_reg_identity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace log_reg_identity.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private IdentityContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ForumController(
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
        [Route("index")]
        public IActionResult Index()
        {
            List<Category> Categories = _context.categories.Include(t => t.Topics).Include(m => m.Moderators).ThenInclude(u => u.User).ToList();
            ViewBag.Categories = Categories;
            
            return View();
        }


        [Route("add/admin")]
        public async Task<IActionResult> AddAdminAsync()
        {
            Task<User> CurrUser = GetCurrentUserAsync();
            User user = CurrUser.Result;
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("category/add")]
        public IActionResult AddCategory(string Name)
        {
            User CurrUser = GetCurrentUserAsync().Result;
            Category NewCategory = new Category
            {
                Name = Name,
                UserId = CurrUser.Id,

            };
            _context.Add(NewCategory);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("category/delete/{Name}")]
        public IActionResult DeleteCategory(string Name)
        {
            Category category = _context.categories.SingleOrDefault(c => c.Name == Name);
            _context.categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{Name}")]
        public IActionResult Show(string Name)
        {
            Category Category = _context.categories.Include(t => t.Topics).ThenInclude(u => u.User).SingleOrDefault(c => c.Name == Name);
            ViewBag.Category = Category;

            return View();
        }

        [HttpPost]
        [Route("topic/add")]
        public IActionResult AddTopic(string Title, string TopicText, int CategoryId)
        {
            User CurrUser = GetCurrentUserAsync().Result;
            string Name = _context.categories.SingleOrDefault(c => c.CategoryId == CategoryId).Name;
            Topic NewTopic = new Topic
            {
                Title = Title,
                TopicText = TopicText,
                UserId = CurrUser.Id,
                CategoryId = CategoryId
            };

            _context.Add(NewTopic);
            _context.SaveChanges();
            return Redirect($"/{Name}");
        }

        [HttpGet]
        [Route("topic/delete/{TopicId}")]
        public IActionResult DeleteTopic(int TopicId)
        {
            Topic TopicDelete = _context.topics.SingleOrDefault(c => c.TopicId == TopicId);
            _context.topics.Remove(TopicDelete);
            _context.SaveChanges();
            
            return RedirectToAction("Show");
        }

        [HttpGet]
        [Route("topic/{TopicId}")]
        public IActionResult Topic(int TopicId)
        {
            ViewBag.User = GetCurrentUserAsync().Result;
            Topic Topic = _context.topics.Include(t => t.User).Include(c => c.Comments).ThenInclude(u => u.User).SingleOrDefault(c => c.TopicId == TopicId);
            ViewBag.Topic = Topic;

            return View("Topic");
        }

        [HttpPost]
        [Route("add/comment")]
        public IActionResult AddTopic(string CommentText, int TopicId)
        {
            User CurrUser = GetCurrentUserAsync().Result;
            Comment NewComment = new Comment
            {
                CommentText = CommentText,
                UserId = CurrUser.Id,
                TopicId = TopicId
            };

            _context.Add(NewComment);
            _context.SaveChanges();

            return Redirect($"/topic/{TopicId}");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("profile")]
        public IActionResult GetProfile()
        {
            User CurrUser = GetCurrentUserAsync().Result;
            return Redirect($"profile/{CurrUser.FirstName}");
        }

        [Authorize(Roles = "Admin")]
        [Route("profile/{FirstName}")]
        public IActionResult Profile()
        {
            ViewBag.User = GetCurrentUserAsync().Result;
            return View();
        }

        // [HttpPost]
        // [Route("edit")]
        // public async Task<IActionResult> EditUserAsync(RegisterViewModel model, string OldPass)
        // {
        //     User EditUser = GetCurrentUserAsync().Result;
        //     if(ModelState.IsValid)
        //     {
        //         var result = await _signInManager.PasswordSignInAsync(EditUser, OldPass, isPersistent: false, lockoutOnFailure: false);
        //         if (result.Succeeded)
        //         {
        //             EditUser.FirstName = model.FirstName;
        //             EditUser.LastName = model.LastName;
        //             EditUser.Email = model.Email;
        //             return RedirectToAction("Index");
        //         }
        //     }
        //     if(OldPass == null)
        //     {
        //         ViewBag.OldPass = "Incorrect Password";
        //     }
        //     ViewBag.User = EditUser;
        //     return View("Profile", model);
        // }
        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}