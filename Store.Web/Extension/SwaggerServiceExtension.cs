using Microsoft.OpenApi.Models;

namespace Store.Web.Extension
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Store API",
                    Version = "v1",
                    Contact =
                    {
                        Name = "Zaki",
                        Email = "zaki@gmail.com",
                        Url = new Uri("#")
                    }

                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "bearer",
                        Type = ReferenceType.SecurityScheme,
                    }
                };

                options.AddSecurityDefinition("bearer", securityScheme);

                var securityRequirements = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] {"bearer"} }
                };

                options.AddSecurityRequirement(securityRequirements);
            });

            return services;
        }
    }
}
