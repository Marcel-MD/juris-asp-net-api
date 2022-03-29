namespace Juris.Domain.Entities;

public class City : BaseEntity
{
    public string Name { get; set; }
    
    public ICollection<Profile> Profiles { get; set; }
}