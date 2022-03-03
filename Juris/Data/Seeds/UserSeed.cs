using Juris.Models.Constants;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Data.Seeds;

public static class UserSeed
{
    public static async Task Seed(UserManager<User> userManager)
    {
        if (userManager.Users.Any()) return;

        Log.Warning("Seeding users data ...");

        var users = new List<User>
        {
            new()
            {
                UserName = "Marcel",
                Email = "marcelvlasenco@gmail.com"
            },
            new()
            {
                UserName = "ViorelNoroc",
                Email = "noroc@mailinator.com"
            },
            new()
            {
                UserName = "NicuSavva",
                Email = "savva@mailinator.com"
            },
            new()
            {
                UserName = "IrinaTiora",
                Email = "tiora@mailinator.com"
            },
            new()
            {
                UserName = "StephyMatvei",
                Email = "stephy@mailinator.com"
            }
        };

        await userManager.CreateAsync(users[0], "admin123");
        await userManager.AddToRoleAsync(users[0], RoleType.Admin);

        for (var i = 1; i < users.Count; i++)
        {
            await userManager.CreateAsync(users[i], "password123");
            await userManager.AddToRoleAsync(users[i], RoleType.User);
        }
    }
}