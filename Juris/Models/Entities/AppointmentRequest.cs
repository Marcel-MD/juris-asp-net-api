using Juris.Models.Enums;

namespace Juris.Models.Entities;

public class AppointmentRequest : BaseEntity
{
    public long UserDashboardId { get; set; }

    public UserDashboard UserDashboard { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public AppointmentStatus Status { get; set; }

    public DateTime CreationDate { get; set; }
}