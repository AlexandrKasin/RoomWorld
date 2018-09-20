using System.Threading;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Amenties> Amentiese { get; set; }
        public DbSet<Extras> Extrase { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Flat> Flat { get; set; }
        public DbSet<HouseRules> HouseRulese { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.ApplyAuditInformation();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
