using Juris.Data.Seeds;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Juris.Data;

public static class DatabaseSeeder
{
    public static async Task Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        
        var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
        var userManger = serviceScope.ServiceProvider.GetService<UserManager<User>>();
        var dbContext = serviceScope.ServiceProvider.GetService<DatabaseContext>();

        if (roleManager == null)
        {
            Console.WriteLine("Role manger is null");
        }

        if (userManger == null)
        {
            Console.WriteLine("User manger is null");
        }
        
        await RoleSeed.Seed(roleManager);
        await UserSeed.Seed(userManger);

        await AppointmentRequestSeed.Seed(dbContext);
        await ProfileSeed.Seed(dbContext);
        await AddressSeed.Seed(dbContext);
        await EducationSeed.Seed(dbContext);
        await ExperienceSeed.Seed(dbContext);
        await ReviewSeed.Seed(dbContext);
    }
}