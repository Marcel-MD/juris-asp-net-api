using Juris.Dal.Seeds;
using Juris.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Juris.Dal;

public static class DatabaseSeeder
{
    /// <summary>
    ///     Seeds random users' data in the database if it's empty. By default it generates 12 users and their profiles and
    ///     three times more educations, experiences, reviews and appointments, all are distributed randomly.
    /// </summary>
    /// <param name="app">Current application builder.</param>
    /// <param name="nrOfUsers">Number of users to be generated.</param>
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