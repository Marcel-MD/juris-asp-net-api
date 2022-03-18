namespace Juris.Models.Entities;

public class ProfileCategory : BaseEntity
{
    public string Category { get; set; }
    
    public ICollection<Profile> Profiles { get; set; }
}