using Juris.Domain.Constants;
using Juris.Domain.Entities;
using Serilog;

namespace Juris.Data.Seeds;

public static class AppointmentRequestSeed
{
    public static async Task Seed(DatabaseContext dbContext)
    {
        if (dbContext.AppointmentRequests.Any()) return;

        Log.Warning("Seeding appointment requests data ...");

        dbContext.AppointmentRequests.AddRange
        (
            new AppointmentRequest
            {
                UserId = 2,
                Email = "mada@mailinator.com",
                FirstName = "MadMary",
                LastName = "Ungureanu",
                PhoneNumber = "060989777",
                Description = "I have some urgent problem. Please help!",
                Status = AppointmentStatus.Approved
            },
            new AppointmentRequest
            {
                UserId = 3,
                Email = "valeria@mailinator.com",
                FirstName = "Valeria",
                LastName = "Something",
                PhoneNumber = "060989123",
                Description = "I need your consultation right now!"
            },
            new AppointmentRequest
            {
                UserId = 2,
                Email = "marcel@mailinator.com",
                FirstName = "Marcel",
                LastName = "Vlasenco",
                PhoneNumber = "060989713",
                Description = "You did a great job last time, I want to hire you again."
            },
            new AppointmentRequest
            {
                UserId = 4,
                Email = "stefan@mailinator.com",
                FirstName = "Stefan",
                LastName = "Boicu",
                PhoneNumber = "060989654",
                Description = "What are you doing tonight?",
                Status = AppointmentStatus.Declined
            }
        );

        await dbContext.SaveChangesAsync();
    }
}