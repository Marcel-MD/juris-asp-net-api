using Microsoft.AspNetCore.Identity;

namespace Juris.Models.Identity;

public class Role : IdentityRole<long>
{
    public virtual ICollection<UserRole> Users { get; set; }
}