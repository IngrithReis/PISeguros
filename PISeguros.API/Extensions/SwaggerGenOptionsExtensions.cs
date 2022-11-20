using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PISeguros.API.Extensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void AddJWTSecurityDefinition(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Authentication", new OpenApiSecurityScheme()
            {
                Name = "Authentication",
                Description = "Token JWT",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            var openApiSecurityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Authentication"
                }
            };

            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement { { openApiSecurityScheme, Array.Empty<string>() } }
            );

        }
    }
}
