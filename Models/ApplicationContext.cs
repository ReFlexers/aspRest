using Microsoft.EntityFrameworkCore;

namespace asp.net_1.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();  
        }
    }
}
