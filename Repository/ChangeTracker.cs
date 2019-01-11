using System;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repository
{
    public static class ChangeTrackerExtensions
    {
        public static void ApplyAuditInformation(this ChangeTracker changeTracker)
        {
            foreach (var entry in changeTracker.Entries())
            {
                if (!(entry.Entity is BaseEntity baseAudit)) continue;

                var now = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        SetRowVersion(entry);
                        baseAudit.ModifiedDate = now;
                        break;
                    case EntityState.Added:
                        baseAudit.CreatedDate = now;
                        break;
                }
            }
        }

        private static void SetRowVersion(EntityEntry entry)
        {
            entry.OriginalValues[nameof(BaseEntity.Version)] = entry.CurrentValues[nameof(BaseEntity.Version)];
        }
    }
}
