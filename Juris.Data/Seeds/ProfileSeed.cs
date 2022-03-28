using Juris.Domain.Constants;
using Juris.Domain.Entities;
using Serilog;

namespace Juris.Data.Seeds;

public static class ProfileSeed
{
    public static async Task Seed(DatabaseContext dbContext)
    {
        if (dbContext.Profiles.Any()) return;

        Log.Warning("Seeding profiles data ...");

        dbContext.Profiles.AddRange
        (
            new Profile
            {
                UserId = 2,
                FirstName = "Viorel",
                LastName = "Noroc",
                PhoneNumber = "060989543",
                Description = "I am a great lawyer, look at me!",
                ProfileCategoryId = 1,
                Status = ProfileStatus.Approved,
                Price = 800,
                Rating = 9,
                CityId = 1,
                Address = "Students str."
            },
            new Profile
            {
                UserId = 3,
                FirstName = "Nicu",
                LastName = "Savva",
                PhoneNumber = "060989678",
                Description = "I can play drums, better than any lawyer out there!",
                ProfileCategoryId = 1,
                Price = 500,
                Rating = 7,
                CityId = 4,
                Address = "OrheiLand str."
            },
            new Profile
            {
                UserId = 4,
                FirstName = "Irina",
                LastName = "Tiora",
                PhoneNumber = "060989974",
                Description = "I am a great at judging food!",
                ProfileCategoryId = 3,
                Status = ProfileStatus.Approved,
                Price = 780,
                Rating = 8.5,
                CityId = 1,
                Address = "Ginta Latina str."
            },
            new Profile
            {
                UserId = 5,
                FirstName = "Stephania",
                LastName = "Matvei",
                PhoneNumber = "060986754",
                Description = "I am the best notary in the town!",
                ProfileCategoryId = 2,
                Status = ProfileStatus.Approved,
                Price = 600,
                Rating = 7.9,
                CityId = 2,
                Address = "Stefan cel Mare str."
            }
        );

        await dbContext.SaveChangesAsync();
    }
}