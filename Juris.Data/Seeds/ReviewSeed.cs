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
                Description =
                    "Best lawyer ever! Also knows programming! Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 9
            },
            new Review
            {
                ProfileId = 2,
                Email = "valeria@mailinator.com",
                FirstName = "Valeria",
                LastName = "Dubina",
                PhoneNumber = "060989123",
                Description =
                    "Very professional! Recommend to everyone! Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 7
            },
            new Review
            {
                ProfileId = 1,
                Email = "marcel@mailinator.com",
                FirstName = "Marcel",
                LastName = "Vlasenco",
                PhoneNumber = "060989713",
                Description =
                    "Saved me a lot fo time and money! Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 10
            },
            new Review
            {
                ProfileId = 3,
                Email = "stefan@mailinator.com",
                FirstName = "Stefan",
                LastName = "Boicu",
                PhoneNumber = "060989654",
                Description =
                    "Had to find someone else! Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 5
            },
            new Review
            {
                ProfileId = 4,
                Email = "coroletchi@mailinator.com",
                FirstName = "Ana",
                LastName = "Coroletchi",
                PhoneNumber = "060189754",
                Description =
                    "Not so good! But clarified some aspects. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 6
            },
            new Review
            {
                ProfileId = 2,
                Email = "mada@mailinator.com",
                FirstName = "MadMary",
                LastName = "Ungureanu",
                PhoneNumber = "060989777",
                Description =
                    "Best lawyer ever! Also knows programming! Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 9
            },
            new Review
            {
                ProfileId = 1,
                Email = "valeria@mailinator.com",
                FirstName = "Valeria",
                LastName = "Dubina",
                PhoneNumber = "060989123",
                Description =
                    "Very professional! Recommend to everyone! Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 7
            },
            new Review
            {
                ProfileId = 3,
                Email = "marcel@mailinator.com",
                FirstName = "Marcel",
                LastName = "Vlasenco",
                PhoneNumber = "060989713",
                Description =
                    "Saved me a lot fo time and money! Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 10
            },
            new Review
            {
                ProfileId = 4,
                Email = "stefan@mailinator.com",
                FirstName = "Stefan",
                LastName = "Boicu",
                PhoneNumber = "060989654",
                Description =
                    "Had to find someone else! Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 5
            },
            new Review
            {
                ProfileId = 5,
                Email = "coroletchi@mailinator.com",
                FirstName = "Ana",
                LastName = "Coroletchi",
                PhoneNumber = "060189754",
                Description =
                    "Not so good! But clarified some aspects. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Rating = 6
            }
        );

        await dbContext.SaveChangesAsync();
    }
}