using Microsoft.EntityFrameworkCore;

namespace User_for__NUnit_Test.Model
{
    public class UserContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserContext(DbContextOptions<UserContext> options): base(options)
        {
        }
    }
}
