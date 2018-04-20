using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//Other usings
namespace log_reg_identity.Models
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
        //Setup Context as normal
        public DbSet<User> users { get; set; }
        public DbSet<Topic> topics { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Category> categories {get;set;}
        public DbSet<Moderator> moderators {get;set;}
    }
}
