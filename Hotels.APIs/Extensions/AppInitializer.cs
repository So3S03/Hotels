using Hotels.Domain.Entities.BaseEntities;
using Hotels.Domain.Entities.Identity;
using Hotels.Domain.Entities.Room;
using Hotels.Infrastructure.Persistence.Data.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hotels.APIs.Extensions
{
    public static class AppInitializer
    {
        public static async Task<IApplicationBuilder> InitApp<TDbContext>(this IApplicationBuilder app)
            where TDbContext : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var DbContext = serviceProvider.GetRequiredService<TDbContext>();
            var LoggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var Logger = LoggerFactory.CreateLogger<Program>();
            var roomsData = DataSeeding.CreateRooms();
            var UserData = DataSeeding.CreatedUser();
            var User = UserData["User"] as AppUser;
            var Password = UserData["Password"] as string;
            if (User is null || Password is null) throw new Exception("Invalid seeding data for Admin user");
            try
            {
                //Applying Pending Migrations
                var pendingMigrations = await DbContext.Database.GetPendingMigrationsAsync();
                if(pendingMigrations.Any())
                {
                    await DbContext.Database.MigrateAsync();
                }
                //Seeding Data
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                var adminExist = await userManager.Users.AnyAsync(u => u.Email == User.Email);
                if(!adminExist)
                {
                    var result = await userManager.CreateAsync(User, Password);
                    if (!result.Succeeded) throw new Exception("Couldn't Seed Admin");
                }
                var RoomsFromDb = await DbContext.Set<Room>().Select(r => r.RoomNumber).ToListAsync();
                var RoomsNotAdded = roomsData.Where(r => !RoomsFromDb.Contains(r.RoomNumber)).ToList();
                if(RoomsNotAdded.Any())
                {
                    //Opening Transaction For Adding The Rooms Alongside With The Log
                    await using var transaction = await DbContext.Database.BeginTransactionAsync();
                    try
                    {
                        //Adding The Rooms
                        await DbContext.Set<Room>().AddRangeAsync(RoomsNotAdded);
                        var roomsResult = await DbContext.SaveChangesAsync() > 0;
                        if (!roomsResult) throw new Exception("Couldn't Seed Rooms");
                        //Adding The Create Log
                        var roomsIds = RoomsNotAdded.Select(r => r.Id).ToList();
                        var auditsLog = roomsIds.Select(id => new AuditLog()
                        {
                            UserId = "SystemId",
                            UserName = "GeneratedBySystem",
                            ActionDate = DateTime.UtcNow,
                            ActionType = ActionType.Created,
                            EntityName = typeof(Room).Name,
                            EntityId = id
                        }).ToList();
                        await DbContext.Set<AuditLog>().AddRangeAsync(auditsLog);
                        var auditResult = await DbContext.SaveChangesAsync() > 0;
                        if (!auditResult) throw new Exception("Couldn't Seed Audit Fields");
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
            return app;
        }
    }
}
