
using Hotels.APIs.Extensions;
using Hotels.APIs.Middlewares;
using Hotels.Application._Common;
using Hotels.Application.Hubs;
using Hotels.Infrastructure.Persistence._Common;
using Hotels.Infrastructure.Persistence.Data.Contexts;

namespace Hotels.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddPersisitence(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(corsOpt =>
            {
                corsOpt.AddPolicy("Front-End-Policy", corsPolicyBuilder =>
                {
                    corsPolicyBuilder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            var app = builder.Build();
            await app.InitApp<ApplicationDbContext>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("Front-End-Policy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapHub<NotificationHub>("/hub/notifications");
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
