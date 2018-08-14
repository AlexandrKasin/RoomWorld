using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
