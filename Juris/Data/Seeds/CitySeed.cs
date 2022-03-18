using Juris.Models.Entities;
using Serilog;

namespace Juris.Data.Seeds;

public class CitySeed
{
    public static async Task Seed(DatabaseContext dbContext)
    {
        if (dbContext.Cities.Any()) return;

        Log.Warning("Seeding cities data ...");

        dbContext.Cities.AddRange
        (
            new City
            {
                Name = "Chisinau"
            },
            new City
            {
                Name = "Balti"
            },
            new City
            {
                Name = "Cahul"
            },
            new City
            {
                Name = "Orhei"
            }
        );

        await dbContext.SaveChangesAsync();
    }
}