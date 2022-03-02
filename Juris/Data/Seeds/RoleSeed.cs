using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Data.Seeds;

public static class RoleSeed
{
    public static async Task Seed(RoleManager<Role> roleManager)
    {
        if(roleManager.Roles.Any()) return;
        
        Log.Information("Seeding roles data ...");
        
        List<Role> roles = new List<Role>
        {
            new Role
            {
                Name = "Admin"
            },
            new Role
            {
                Name = "User"
            }
        };

        for (int i = 0; i < roles.Count; i++)
        {
            await roleManager.CreateAsync(roles[i]);
        }
    }
}