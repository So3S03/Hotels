using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Identity;
using Hotels.Infrastructure.Persistence.Data.Contexts;
using Hotels.Infrastructure.Persistence.Data.Interceptors;
using Hotels.Infrastructure.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotels.Infrastructure.Persistence._Common
{
    public static class PersistenceDI
    {
        public static IServiceCollection AddPersisitence(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultDatabase");
            service.AddDbContext<ApplicationDbContext>((serviceProvider ,options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<AuditLogsInterceptor>();
                options.UseSqlServer(connectionString);
                options.AddInterceptors(interceptor);
            })
                .AddIdentityCore<AppUser>(identityOptions =>
                {
                    identityOptions.User.RequireUniqueEmail = true;
                    identityOptions.Password.RequireNonAlphanumeric = true;
                    identityOptions.Password.RequiredLength = 8;
                    identityOptions.Password.RequireLowercase = true;
                    identityOptions.Password.RequireDigit = true;
                    identityOptions.Password.RequireUppercase = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
            service.AddHttpContextAccessor();
            return service;
        }
    }
}
