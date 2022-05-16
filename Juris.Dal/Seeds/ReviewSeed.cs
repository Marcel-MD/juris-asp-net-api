using Bogus;
using Juris.Domain.Entities;
using Serilog;

namespace Juris.Dal.Seeds;

public static class ReviewSeed
{
    public static async Task Seed(DatabaseContext dbContext, int n)
    {
        if (dbContext.Reviews.Any()) return;
        Log.Warning("Seeding reviews data ...");

        var reviewFaker = new Faker<Review>()
            .RuleFor(r => r.ProfileId, f => f.Random.Long(1, n))
            .RuleFor(r => r.FirstName, f => f.Name.FirstName())
            .RuleFor(r => r.LastName, f => f.Name.LastName())
            .RuleFor(r => r.Email, (f, r) => f.Internet.Email(r.FirstName, r.LastName, "mailinator.com"))
            .RuleFor(r => r.PhoneNumber, f => f.Random.Replace("0#########"))
            .RuleFor(r => r.Description, f => f.Lorem.Sentences(2))
            .RuleFor(r => r.Rating, f => f.Random.Int(3, 10));

        var reviews = reviewFaker.Generate(n * 3);

        dbContext.Reviews.AddRange(reviews);

        await dbContext.SaveChangesAsync();
    }
}