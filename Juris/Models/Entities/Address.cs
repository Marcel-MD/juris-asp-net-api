using Juris.Models.Enums;

namespace Juris.Models.Entities;

public class Address : BaseEntity
{
    public City City { get; set; }
    
    public string AddressLine { get; set; }
}