using Juris.Dal.Seeds;
using Juris.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Juris.Dal;

public static class DatabaseSeeder
{
    public static async Task Seed(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
        var userManger = serviceScope.ServiceProvider.GetService<UserManager<User>>();
        var dbContext = serviceScope.ServiceProvider.GetService<DatabaseContext>();

        await RoleSeed.Seed(roleManager);
        await UserSeed.Seed(userManger);

        await ProfileCategorySeed.Seed(dbContext);
        await CitySeed.Seed(dbContext);
        await AppointmentRequestSeed.Seed(dbContext);
        await ProfileSeed.Seed(dbContext);
        await EducationSeed.Seed(dbContext);
        await ExperienceSeed.Seed(dbContext);
        await ReviewSeed.Seed(dbContext);
    }
}