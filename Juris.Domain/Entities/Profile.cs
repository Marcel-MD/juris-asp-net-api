using Juris.Domain.Identity;

namespace Juris.Domain.Entities;

public class Profile : BaseEntity
{
    public long UserId { get; set; }

    public User User { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public int Price { get; set; }

    public double Rating { get; set; }

    public string Address { get; set; }

    public long CityId { get; set; }

    public City City { get; set; }

    public long ProfileCategoryId { get; set; }

    public ProfileCategory ProfileCategory { get; set; }

    public ICollection<Education> Educations { get; set; }

    public ICollection<Experience> Experiences { get; set; }

    public ICollection<Review> Reviews { get; set; }
}