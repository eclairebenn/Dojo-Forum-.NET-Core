using System.Collections.Generic;

namespace log_reg_identity.Models
{
    public class Topic : BaseEntity
    {
        public int TopicId {get;set;}
        public string Title {get; set;}

        public string TopicText {get;set;}
        public string UserId { get; set; }
        public User User { get; set; }
        public int CategoryId {get;set;}
        public Category Category {get;set;}
        public List<Comment> Comments {get;set;}
        
        public Topic()
        {
            Comments = new List<Comment>();
        }
    }
}
