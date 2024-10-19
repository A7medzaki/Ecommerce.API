using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public static class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Zaki",
                    Email = "Zaki@gmail.com",
                    UserName = "Zaki",
                    Address = new Address
                    {
                        FirstName = "Ahmed",
                        LastName = "Zaki",
                        City = "Marg",
                        State = "Cairo",
                        Street = "10",
                        PostalCode = "12345"
                    }
                };

                await userManager.CreateAsync(user, "Password123!");
            }
        }
    }
}
