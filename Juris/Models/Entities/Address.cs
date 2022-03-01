using Juris.Models.Enums;

namespace Juris.Models.Entities;

public class Address : BaseEntity
{
    public long ProfileId { get; set; }

    public Profile Profile { get; set; }

    public City City { get; set; }

    public string AddressLine { get; set; }
}