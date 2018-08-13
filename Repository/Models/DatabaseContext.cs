using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RoomWorld.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=WS-018-41\SQLEXPRESS;Initial Catalog=RoomWorld;Persist Security Info=True;User ID=sa;Password=2831765");
        }
    }
}
