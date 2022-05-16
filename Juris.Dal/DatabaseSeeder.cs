using Juris.Dal.Seeds;
using Juris.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Juris.Dal;

public static class DatabaseSeeder
{
    public static async Task Seed(this IApplicationBuilder app, int nrOfUsers = 12)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
        var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
        var dbContext = serviceScope.ServiceProvider.GetService<DatabaseContext>();

        if (userManager == null || userManager.Users.Any()) return;

        await RoleSeed.Seed(roleManager);
        await ProfileCategorySeed.Seed(dbContext);
        await CitySeed.Seed(dbContext);

        await UserProfileSeed.Seed(userManager, dbContext, nrOfUsers);
        await AppointmentRequestSeed.Seed(dbContext, nrOfUsers);
        await EducationSeed.Seed(dbContext, nrOfUsers);
        await ExperienceSeed.Seed(dbContext, nrOfUsers);
        await ReviewSeed.Seed(dbContext, nrOfUsers);
    }
}