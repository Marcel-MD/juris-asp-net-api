using Bogus;
using Juris.Domain.Constants;
using Juris.Domain.Entities;
using Juris.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Dal.Seeds;

internal class FakeUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

public static class UserProfileSeed
{
    public static async Task Seed(UserManager<User> userManager, DatabaseContext dbContext, int n)
    {
        if (userManager.Users.Any()) return;
        Log.Warning("Seeding users and profiles data ...");

        var userFaker = new Faker<FakeUser>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName, "mailinator.com"));

        var profileFaker = new Faker<Profile>()
            .RuleFor(p => p.Address, f => f.Address.StreetAddress())
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
            .RuleFor(p => p.Price, f => f.Random.Int(100, 2000))
            .RuleFor(p => p.Rating, f => f.Random.Int(5, 10))
            .RuleFor(p => p.PhoneNumber, f => f.Random.Replace("0#########"))
            .RuleFor(p => p.Status,
                f => f.Random.ArrayElement(new[] {ProfileStatus.Approved, ProfileStatus.Unapproved}))
            .RuleFor(p => p.CityId, f => f.Random.Long(1, 3))
            .RuleFor(p => p.ProfileCategoryId, f => f.Random.Long(1, 3));

        var admin = new User
        {
            UserName = "admin@mailinator.com",
            Email = "admin@mailinator.com"
        };
        await userManager.CreateAsync(admin, "admin123");
        await userManager.AddToRoleAsync(admin, RoleType.Admin);

        for (var i = 0; i < n; i++)
        {
            var fakeUser = userFaker.Generate();
            var user = new User
            {
                UserName = fakeUser.Email,
                Email = fakeUser.Email
            };
            await userManager.CreateAsync(user, "password123");
            await userManager.AddToRoleAsync(user, RoleType.User);

            var fakeProfile = profileFaker.Generate();
            fakeProfile.FirstName = fakeUser.FirstName;
            fakeProfile.LastName = fakeUser.LastName;
            fakeProfile.UserId = user.Id;

            await dbContext.Profiles.AddAsync(fakeProfile);
        }

        await dbContext.SaveChangesAsync();
    }
}