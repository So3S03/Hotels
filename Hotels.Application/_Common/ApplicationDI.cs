using Hotels.Application.Abstraction._Common.Contracts;
using Hotels.Application.Abstraction.ServicesContracts;
using Hotels.Application.Notifications;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Hotels.Application._Common
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped(typeof(IUserService), typeof(UserService));
            service.AddMediatR(configs =>
            {
                configs.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly);
            });
            service.AddAuthentication(configOptions =>
            {
                configOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                configOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configOpt =>
            {
                var jwtConfigs = configuration.GetSection("JwtConfigs");
                configOpt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = jwtConfigs.GetValue<string>("Audience"),

                    ValidateIssuer = true,
                    ValidIssuer = jwtConfigs.GetValue<string>("Issuer"),

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.GetValue<string>("SecretKey")!)),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                configOpt.Events = new JwtBearerEvents()
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var error = JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "You are not authenticated, please provide valid token"
                        });
                        await context.Response.WriteAsync(error);
                    },

                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var error = JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "You are not authorized to access this resource"
                        });
                        await context.Response.WriteAsync(error);
                    }
                };
            });
            var globalSettings = TypeAdapterConfig.GlobalSettings;
            globalSettings.Scan(typeof(ApplicationAssembly).Assembly);
            service.AddSingleton(globalSettings);
            service.AddScoped<IMapper, ServiceMapper>();
            service.AddSignalR();
            service.AddScoped(typeof(IRoomNotificationService), typeof(RoomNotificationService));
            service.AddScoped(typeof(IReservationNotificationService), typeof(ReservationNotificationService));
            return service;
        }
    }
}
