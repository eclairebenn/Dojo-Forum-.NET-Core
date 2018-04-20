using System;
using System.Collections.Generic;

namespace log_reg_identity.Models
{
    public class Category : BaseEntity
    {
        public int CategoryId {get;set;}
        public string Name {get; set;}
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Topic> Topics {get;set;}
        public List<Moderator> Moderators {get;set;}

        public Category()
        {
            Topics = new List<Topic>();
            Moderators = new List<Moderator>();
        }
    }
}