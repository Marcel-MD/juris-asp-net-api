using Juris.Domain.Entities;
using Serilog;

namespace Juris.Dal.Seeds;

public class ProfileCategorySeed
{
    public static async Task Seed(DatabaseContext dbContext)
    {
        if (dbContext.ProfileCategories.Any()) return;

        Log.Warning("Seeding profile categories data ...");

        dbContext.ProfileCategories.AddRange
        (
            new ProfileCategory
            {
                Category = "Lawyer"
            },
            new ProfileCategory
            {
                Category = "Notary"
            },
            new ProfileCategory
            {
                Category = "Judge"
            }
        );

        await dbContext.SaveChangesAsync();
    }
}