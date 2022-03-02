using Juris.Models.Enums;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Data.Seeds;

    public static class UserSeed
    {
        public static async Task Seed(UserManager<User> userManager)
        {
            if (userManager.Users.Any()) return;
            
            Log.Information("Seeding users data ...");
            
            List<User> users = new List<User>
            {
                new User
                {
                    UserName = "Marcel",
                    Email = "marcelvlasenco@gmail.com"
                },
                new User
                {
                    UserName = "ViorelNoroc",
                    Email = "noroc@mailinator.com"
                },
                new User
                {
                    UserName = "NicuSavva",
                    Email = "savva@mailinator.com"
                },
                new User
                {
                    UserName = "IrinaTiora",
                    Email = "tiora@mailinator.com"
                },
                new User
                {
                    UserName = "StephyMatvei",
                    Email = "stephy@mailinator.com"
                }
            };
            
            await userManager.CreateAsync(users[0], "admin123");
            await userManager.AddToRoleAsync(users[0], RoleConst.Admin);

            for (int i = 1; i < users.Count; i++)
            {
                await userManager.CreateAsync(users[i], "password123");
                await userManager.AddToRoleAsync(users[i], RoleConst.User);
            }
        }
    }