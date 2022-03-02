using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Data.Seeds;

public static class RoleSeed
{
    public static async Task Seed(RoleManager<Role> roleManager)
    {
        if (roleManager.Roles.Any()) return;

        Log.Warning("Seeding roles data ...");

        var roles = new List<Role>
        {
            new()
            {
                Name = "Admin"
            },
            new()
            {
                Name = "User"
            }
        };

        for (var i = 0; i < roles.Count; i++) await roleManager.CreateAsync(roles[i]);
    }
}