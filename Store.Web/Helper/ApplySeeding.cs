﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities.IdentityEntities;
using Store.Repository;

namespace Store.Web.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<StoreDbContext>();

                    var userManager = services.GetRequiredService<UserManager<AppUser>>();

                    if (context.Database.IsRelational())
                    {
                        await context.Database.MigrateAsync();
                    }
                    await StoreContextSeed.SeedAsync(context, loggerFactory);

                    await StoreIdentityContextSeed.SeedUserAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplySeeding>();

                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
