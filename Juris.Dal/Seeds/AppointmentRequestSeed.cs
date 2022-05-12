using Bogus;
using Juris.Domain.Constants;
using Juris.Domain.Entities;
using Serilog;

namespace Juris.Dal.Seeds;

public static class AppointmentRequestSeed
{
    public static async Task Seed(DatabaseContext dbContext, int n)
    {
        if (dbContext.AppointmentRequests.Any()) return;
        Log.Warning("Seeding appointment requests data ...");

        var appointmentFaker = new Faker<AppointmentRequest>()
            .RuleFor(a => a.UserId, f => f.Random.Long(2, n))
            .RuleFor(a => a.FirstName, f => f.Name.FirstName())
            .RuleFor(a => a.LastName, f => f.Name.LastName())
            .RuleFor(a => a.Email, (f, a) => f.Internet.Email(a.FirstName, a.LastName, "mailinator.com"))
            .RuleFor(a => a.PhoneNumber, f => f.Random.Replace("0#########"))
            .RuleFor(a => a.Description, f => f.Lorem.Paragraph())
            .RuleFor(a => a.Status,
                f => f.Random.ArrayElement(new[]
                    {AppointmentStatus.Approved, AppointmentStatus.Declined, AppointmentStatus.OnHold}));

        var appointmentRequests = appointmentFaker.Generate(n * 3);

        dbContext.AppointmentRequests.AddRange(appointmentRequests);

        await dbContext.SaveChangesAsync();
    }
}