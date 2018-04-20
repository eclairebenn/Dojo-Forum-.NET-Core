using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace log_reg_identity.Models
{
    public class Moderator : BaseEntity
    {
        public int ModeratorId {get;set;}
        public string UserId { get; set; }
        public User User { get; set; }
        public int CategoryId {get;set;}
        public Category Category {get;set;}
    }
}