using System.Threading;
using System.Threading.Tasks;
using Data.Entity;
using Data.Entity.ApartmentEntity;
using Data.Entity.ChatEntity;
using Data.Entity.UserEntity;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace Repository.Models
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<Amenities> Amenitiese { get; set; }
        public DbSet<ApartmentLocation> ApartmentLocation { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<RulesOfResidence> RulesOfResidence { get; set; }
        public DbSet<ApartmentReservation> ApartmentReservation { get; set; }
        public DbSet<ApartmentImage> ApartmentImages { get; set; }

        
        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }

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