namespace Juris.Domain.Entities;

public class ProfileCategory : BaseEntity
{
    public string Category { get; set; }
    
    public ICollection<Profile> Profiles { get; set; }
}