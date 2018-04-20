using System;

namespace log_reg_identity.Models
{
    public abstract class BaseEntity 
    {
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}