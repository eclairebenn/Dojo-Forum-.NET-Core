using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace log_reg_identity.Models
{
    public class User : IdentityUser
    {
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        public List<Topic> Topics { get; set; }
        public List<Comment> Comments {get;set;}

        public List<Category> Category {get;set;}

        public List<Moderator> Moderators {get;set;}
        
        public User()
        {
            Topics = new List<Topic>();
            Comments = new List<Comment>();
            Category = new List<Category>();
            Moderators = new List<Moderator>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
