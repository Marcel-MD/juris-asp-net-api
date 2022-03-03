using Microsoft.AspNetCore.Identity;

namespace Juris.Models.Identity;

public class UserRole : IdentityUserRole<long>
{
    public virtual Role Role { get; set; }
    public virtual User User { get; set; }
}