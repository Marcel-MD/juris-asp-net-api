using Juris.Models.Entities;
using Juris.Models.Enums;
using Serilog;

namespace Juris.Data.Seeds;

public static class ProfileSeed
{
    public static async Task Seed(DatabaseContext dbContext)
    {
        if (dbContext.Profiles.Any()) return;
        
        Log.Information("Seeding profiles data ...");
        
        dbContext.Profiles.AddRange
        (
            new Profile()
            {
                Id = 2,
                UserId = 2,
                FirstName = "Viorel",
                LastName = "Noroc",
                PhoneNumber = "060989543",
                Description = "I am a great lawyer, look at me!",
                ProfileType = ProfileType.Lawyer,
                Status = ProfileStatus.Approved,
                Price = 800,
                Rating = 9
            },
            new Profile()
            {
                Id = 3,
                UserId = 3,
                FirstName = "Nicu",
                LastName = "Savva",
                PhoneNumber = "060989678",
                Description = "I can play drums, better than any lawyer out there!",
                ProfileType = ProfileType.Lawyer,
                Price = 500,
                Rating = 7
            },
            new Profile()
            {
                Id = 4,
                UserId = 4,
                FirstName = "Irina",
                LastName = "Tiora",
                PhoneNumber = "060989974",
                Description = "I am a great at judging food!",
                ProfileType = ProfileType.Judge,
                Status = ProfileStatus.Approved,
                Price = 780,
                Rating = 8.5,
            },
            new Profile()
            {
                Id = 5,
                UserId = 5,
                FirstName = "Stephania",
                LastName = "Matvei",
                PhoneNumber = "060986754",
                Description = "I am the best notary in the town!",
                ProfileType = ProfileType.Notary,
                Status = ProfileStatus.Approved,
                Price = 600,
                Rating = 7.9
            }
        );

        await dbContext.SaveChangesAsync();
    }
}