namespace log_reg_identity.Models
{
    public class Comment : BaseEntity
    {
        public int CommentId {get;set;}
        public string CommentText {get; set;}
        public string UserId { get; set; }
        public User User { get; set; }
        public int TopicId {get;set;}
        public Topic Topic {get;set;}
    }
}
