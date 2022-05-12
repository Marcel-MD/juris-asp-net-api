using Bogus;
using Juris.Domain.Entities;
using Serilog;

namespace Juris.Dal.Seeds;

public static class ExperienceSeed
{
    public static async Task Seed(DatabaseContext dbContext, int n)
    {
        if (dbContext.Experiences.Any()) return;
        Log.Warning("Seeding experiences data ...");

        var companies = new[]
            {"Amdaris", "Crunchyroll", "Endava", "ISD", "Pentalog", "Code Factory", "Orange", "Google", "Facebook"};
        var positions = new[]
            {"Lawyer", "Notary", "Record Clerk", "Secretary", "Bookkeeper", "Accountant", "Attorney", "Manager"};

        var educationFaker = new Faker<Experience>()
            .RuleFor(e => e.ProfileId, f => f.Random.Long(1, n))
            .RuleFor(e => e.Company, f => f.Random.ArrayElement(companies))
            .RuleFor(e => e.Position, f => f.Random.ArrayElement(positions))
            .RuleFor(e => e.StartDate, f => f.Date.Past())
            .RuleFor(e => e.EndDate, f => f.Random.Bool() ? f.Date.Past() : null);

        var educations = educationFaker.Generate(n * 3);

        dbContext.Experiences.AddRange(educations);

        await dbContext.SaveChangesAsync();
    }
}