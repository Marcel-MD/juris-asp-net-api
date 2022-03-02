using Juris.Models.Entities;
using Serilog;

namespace Juris.Data.Seeds;

public static class ExperienceSeed
{
    public static async Task Seed(DatabaseContext dbContext)
    {
        if (dbContext.Experiences.Any()) return;
        
        Log.Information("Seeding experiences data ...");

        dbContext.Experiences.AddRange
        (
            new Experience()
            {
                ProfileId = 2,
                Company = "Amdaris",
                Position = "Junior Developer",
                StartDate = new DateTime(2021, 9, 1)
            },
            new Experience()
            {
                ProfileId = 3,
                Company = "OrheiLand",
                Position = "Drummer",
                StartDate = new DateTime(2020, 9, 12)
            },
            new Experience()
            {
                ProfileId = 2,
                Company = "Sigmoid",
                Position = "ML Engineer",
                StartDate = new DateTime(2020, 5, 22),
                EndDate = new DateTime(2021, 10, 7)
            },
            new Experience()
            {
                ProfileId = 4,
                Company = "MCDonald",
                Position = "Food Judge",
                StartDate = new DateTime(2019, 7, 13)
            }
        );

        await dbContext.SaveChangesAsync();
    }
}