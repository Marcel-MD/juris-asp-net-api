using Juris.Models.Identity;

namespace Juris.Models.Entities
{
    public abstract class UserDashboard
    {
        public long UserId { get; set; }

        public User User { get; set; }

        public ICollection<AppointmentRequest> appointmentRequests { get; set; }
    }
}
