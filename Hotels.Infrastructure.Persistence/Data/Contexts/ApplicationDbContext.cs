using Hotels.Domain.Entities.BaseEntities;
using Hotels.Domain.Entities.Identity;
using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;
using Hotels.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Infrastructure.Persistence.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        : IdentityDbContext<AppUser, IdentityRole, string>(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(PersistenceAssemply).Assembly);
        }

        //Tables
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
