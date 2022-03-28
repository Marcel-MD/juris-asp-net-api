using Juris.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Juris.Domain.Identity;

public class User : IdentityUser<long>
{
    public Profile Profile { get; set; }
    public ICollection<AppointmentRequest> AppointmentRequests { get; set; }
    public virtual ICollection<UserRole> Roles { get; set; }
    public DateTime CreationDate { get; set; }
}