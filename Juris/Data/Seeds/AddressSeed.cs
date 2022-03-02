using Juris.Models.Entities;
using Juris.Models.Enums;
using Serilog;

namespace Juris.Data.Seeds;

public static class AddressSeed
{
    public static async Task Seed(DatabaseContext dbContext)
    {
        if (dbContext.Addresses.Any()) return;

        Log.Information("Seeding addresses data ...");
        
        dbContext.Addresses.AddRange
        (
            new Address()
            {
                ProfileId = 2,
                City = City.Chisinau,
                AddressLine = "Students str."
            },
            new Address()
            {
                ProfileId = 3,
                City = City.Orhei,
                AddressLine = "OrheiLand str."
            },
            new Address()
            {
                ProfileId = 4,
                City = City.Chisinau,
                AddressLine = "Ginta Latina str."
            },
            new Address()
            {
                ProfileId = 5,
                City = City.Balti,
                AddressLine = "Stefan cel Mare str."
            }
        );

        await dbContext.SaveChangesAsync();
    }
}