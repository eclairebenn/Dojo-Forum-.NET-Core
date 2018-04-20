using System;
using System.ComponentModel.DataAnnotations;

namespace log_reg_identity.Models
{
    public class LoginViewModel : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required(ErrorMessage="Invalid Password")]
        [DataType(DataType.Password)]
        
        public string Password {get;set;}

    }
}