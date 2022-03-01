using Juris.Models.Enums;
using Juris.Models.Identity;

namespace Juris.Models.Entities;

public class AppointmentRequest : BaseEntity
{
    public long UserId { get; set; }

    public User User { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public AppointmentStatus Status { get; set; }

    public DateTime CreationDate { get; set; }
}