using Microsoft.AspNetCore.Identity;

namespace Juris.Domain.Identity;

public class Role : IdentityRole<long>
{
    public virtual ICollection<UserRole> Users { get; set; }
}