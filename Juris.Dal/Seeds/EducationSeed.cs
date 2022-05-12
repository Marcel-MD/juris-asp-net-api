using Bogus;
using Juris.Domain.Entities;
using Serilog;

namespace Juris.Dal.Seeds;

public static class EducationSeed
{
    public static async Task Seed(DatabaseContext dbContext, int n)
    {
        if (dbContext.Educations.Any()) return;
        Log.Warning("Seeding educations data ...");

        var institutions = new[] {"UTM", "ASEM", "USM", "ULIM"};
        var specialities = new[]
        {
            "International Law", "Civil Rights", "Corporate Law", "Criminal Law", "Family Law",
            "Intellectual Property Law", "Employment Law", "Real Estate Law"
        };

        var educationFaker = new Faker<Education>()
            .RuleFor(e => e.ProfileId, f => f.Random.Long(1, n))
            .RuleFor(e => e.Institution, f => f.Random.ArrayElement(institutions))
            .RuleFor(e => e.Speciality, f => f.Random.ArrayElement(specialities))
            .RuleFor(e => e.StartDate, f => f.Date.Past())
            .RuleFor(e => e.EndDate, f => f.Random.Bool() ? f.Date.Past() : null);

        var educations = educationFaker.Generate(n * 3);

        dbContext.Educations.AddRange(educations);

        await dbContext.SaveChangesAsync();
    }
}