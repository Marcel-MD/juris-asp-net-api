using Juris.Domain.Entities;
using Serilog;

namespace Juris.Data.Seeds;

public static class ReviewSeed
{
    public static async Task Seed(DatabaseContext dbContext)
    {
        if (dbContext.Reviews.Any()) return;

        Log.Warning("Seeding reviews data ...");

        dbContext.Reviews.AddRange
        (
            new Review
            {
                ProfileId = 1,
                Email = "mada@mailinator.com",
                FirstName = "MadMary",
                LastName = "Ungureanu",
                PhoneNumber = "060989777",
                Description = "Best lawyer ever! Also knows programming!",
                Rating = 9
            },
            new Review
            {
                ProfileId = 2,
                Email = "valeria@mailinator.com",
                FirstName = "Valeria",
                LastName = "Something",
                PhoneNumber = "060989123",
                Description = "Very professional! Recommend to everyone!",
                Rating = 7
            },
            new Review
            {
                ProfileId = 1,
                Email = "marcel@mailinator.com",
                FirstName = "Marcel",
                LastName = "Vlasenco",
                PhoneNumber = "060989713",
                Description = "Saved me a lot fo time and money!",
                Rating = 10
            },
            new Review
            {
                ProfileId = 3,
                Email = "stefan@mailinator.com",
                FirstName = "Stefan",
                LastName = "Boicu",
                PhoneNumber = "060989654",
                Description = "Had to find someone else!",
                Rating = 5
            }
        );

        await dbContext.SaveChangesAsync();
    }
}