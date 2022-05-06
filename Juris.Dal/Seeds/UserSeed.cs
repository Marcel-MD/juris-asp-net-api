using Juris.Domain.Constants;
using Juris.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Dal.Seeds;

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
                UserName = "marcelvlasenco@gmail.com",
                Email = "marcelvlasenco@gmail.com"
            },
            new()
            {
                UserName = "noroc@mailinator.com",
                Email = "noroc@mailinator.com"
            },
            new()
            {
                UserName = "savva@mailinator.com",
                Email = "savva@mailinator.com"
            },
            new()
            {
                UserName = "tiora@mailinator.com",
                Email = "tiora@mailinator.com"
            },
            new()
            {
                UserName = "stephy@mailinator.com",
                Email = "stephy@mailinator.com"
            },
            new()
            {
                UserName = "mocanu@mailinator.com",
                Email = "mocanu@mailinator.com"
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