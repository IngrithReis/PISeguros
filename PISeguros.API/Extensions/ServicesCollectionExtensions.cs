using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PISeguros.API.Database;
using PISeguros.API.Services;
using PISeguros.API.Settings;
using System.Text;

namespace PISeguros.API.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("AppDbContext")));
            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 4;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders();

            services.Configure<JWTSettings>(configuration.GetSection(nameof(JWTSettings)));
            services.AddScoped<JWTService>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();

            if (settings is null)
                throw new ArgumentException($"Configuração em appsettings [{nameof(JWTSettings)}] não foi encontrada");

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.IssuerSigningKey));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = settings.Audience,
                    ValidateAudience = true,
                    IssuerSigningKey = key,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
