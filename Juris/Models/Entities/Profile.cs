using Juris.Models.Enums;
using Juris.Models.Identity;

namespace Juris.Models.Entities;

public class Profile : BaseEntity
{
    public long UserId { get; set; }

    public User User { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public Category Category { get; set; }

    public ProfileStatus Status { get; set; }

    public long AddressId { get; set; }

    public Address Address { get; set; }

    public int Price { get; set; }

    public ICollection<Education> Educations { get; set; }

    public ICollection<Experience> Experiences { get; set; }

    public ICollection<Review> Reviews { get; set; }
}