using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PISeguros.API.Database;
using PISeguros.API.Extensions;

namespace PISeguros.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Services
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAppServices(builder.Configuration);
            builder.Services.ConfigureAuthentication(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PISeguros.API",
                    Version = "v1"
                });

                options.AddJWTSecurityDefinition();
            });

            // Pipeline
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PISeguros.API v1");
                c.DisplayRequestDuration();
                c.RoutePrefix = string.Empty;
            }); ;

            using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.Migrate();

                SetupIdentity(serviceScope.ServiceProvider).Wait();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static async Task SetupIdentity(IServiceProvider serviceProvider)
        {
            using var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] rolesNames = { "admin" };

            IdentityResult result;

            foreach (var namesRole in rolesNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(namesRole);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new IdentityRole(namesRole));
                }
            }

            var admin = await userManager.FindByNameAsync("admin");

            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com.br",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "#Admin*2022");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}