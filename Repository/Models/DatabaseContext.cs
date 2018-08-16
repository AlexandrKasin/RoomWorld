using System.Threading;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
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
