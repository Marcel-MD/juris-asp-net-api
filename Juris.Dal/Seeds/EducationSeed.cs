using Juris.Domain.Entities;
using Serilog;

namespace Juris.Dal.Seeds;

public static class EducationSeed
{
    public static async Task Seed(DatabaseContext dbContext)
    {
        if (dbContext.Educations.Any()) return;

        Log.Warning("Seeding educations data ...");

        dbContext.Educations.AddRange
        (
            new Education
            {
                ProfileId = 1,
                Institution = "UTM",
                Speciality = "Software Engineering",
                StartDate = new DateTime(2020, 9, 1),
                EndDate = new DateTime(2024, 5, 12)
            },
            new Education
            {
                ProfileId = 2,
                Institution = "USM",
                Speciality = "Lawyer",
                StartDate = new DateTime(2019, 9, 10),
                EndDate = new DateTime(2023, 4, 30)
            },
            new Education
            {
                ProfileId = 1,
                Institution = "ASEM",
                Speciality = "Economy",
                StartDate = new DateTime(2016, 8, 10),
                EndDate = new DateTime(2020, 5, 30)
            },
            new Education
            {
                ProfileId = 3,
                Institution = "UTM",
                Speciality = "Aerospace",
                StartDate = new DateTime(2020, 9, 1),
                EndDate = new DateTime(2024, 5, 12)
            },
            new Education
            {
                ProfileId = 4,
                Institution = "ULIM",
                Speciality = "Foreign Languages",
                StartDate = new DateTime(2021, 9, 1),
                EndDate = new DateTime(2025, 5, 12)
            },
            new Education
            {
                ProfileId = 5,
                Institution = "UTM",
                Speciality = "Software Engineering",
                StartDate = new DateTime(2020, 9, 1),
                EndDate = new DateTime(2024, 5, 12)
            },
            new Education
            {
                ProfileId = 3,
                Institution = "USM",
                Speciality = "Lawyer",
                StartDate = new DateTime(2019, 9, 10),
                EndDate = new DateTime(2023, 4, 30)
            },
            new Education
            {
                ProfileId = 2,
                Institution = "ASEM",
                Speciality = "Economy",
                StartDate = new DateTime(2016, 8, 10),
                EndDate = new DateTime(2020, 5, 30)
            },
            new Education
            {
                ProfileId = 4,
                Institution = "UTM",
                Speciality = "Aerospace",
                StartDate = new DateTime(2020, 9, 1),
                EndDate = new DateTime(2024, 5, 12)
            },
            new Education
            {
                ProfileId = 1,
                Institution = "ULIM",
                Speciality = "Foreign Languages",
                StartDate = new DateTime(2021, 9, 1),
                EndDate = new DateTime(2025, 5, 12)
            }
        );

        await dbContext.SaveChangesAsync();
    }
}