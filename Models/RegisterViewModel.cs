using System;
using System.ComponentModel.DataAnnotations;

namespace log_reg_identity.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        public string UserName {get;set;}
        
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", 
         ErrorMessage = "First name can only contain letters")]
        [Display(Name = "First Name")]
        public string FirstName {get; set;}

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", 
         ErrorMessage = "Last name can only contain letters")]
        [Display(Name = "Last Name")]
        public string LastName {get; set;}

        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage="Password and Password Confirmation must match")]
        [Display(Name = "Password Confirmation")]
        [DataType(DataType.Password)]
        public string PasswordConf {get;set;}

    }
}