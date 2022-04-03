namespace Juris.Domain.Entities;

public class ProfileCategory : BaseEntity
{
    public string Category { get; set; }

    public virtual ICollection<Profile> Profiles { get; set; }
}