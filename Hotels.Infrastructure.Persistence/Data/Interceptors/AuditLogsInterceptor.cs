using Hotels.Application.Abstraction._Common.Contracts;
using Hotels.Domain.Entities.BaseEntities;
using Hotels.Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Hotels.Infrastructure.Persistence.Data.Interceptors
{
    public class AuditLogsInterceptor(IUserService _userService) : SaveChangesInterceptor
    {
        private List<(EntityEntry Entry, EntityState State)>? _entries;
        private void setEntries(DbContext? context)
        {
            if (context is null) return;
            _entries = context.ChangeTracker.Entries()
                .Where(e => e.Entity is not AuditLog && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
                .Select(e => (e, e.State))
                .ToList();
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            setEntries(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            setEntries(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            setAuditLogs(eventData.Context).GetAwaiter().GetResult();
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            setAuditLogs(eventData.Context).GetAwaiter().GetResult();
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private async Task setAuditLogs(DbContext? context)
        {
            if(context is null || _entries is null || _entries.Count == 0) return;
            var logs = _entries.Select(e => new AuditLog()
            {
                ActionDate = DateTime.UtcNow,
                ActionType = e.State == EntityState.Added ? ActionType.Created 
                : e.Entry.Entity is Reservation res && res.Status == ReservationStatus.Cancelled && e.State == EntityState.Modified ? ActionType.Canceled
                : e.State == EntityState.Modified ? ActionType.Updated 
                : ActionType.Deleted,
                UserId = _userService.UserId,
                UserName = _userService.UserName,
                EntityId = e.Entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue?.ToString() ?? string.Empty,
                EntityName = e.Entry.Entity.GetType().Name
            }).ToList();
            await context.Set<AuditLog>().AddRangeAsync(logs);
            var result = await context.SaveChangesAsync();
            if(result <= 0) throw new Exception("Failed To Add Audit Logs");
        }
    }
}
