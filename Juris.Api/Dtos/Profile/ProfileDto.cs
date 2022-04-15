using Juris.Api.Dtos.City;
using Juris.Api.Dtos.Education;
using Juris.Api.Dtos.Experience;
using Juris.Api.Dtos.ProfileCategory;
using Juris.Api.Dtos.Review;

namespace Juris.Api.Dtos.Profile;

public class ProfileDto
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Description { get; set; }

    public string PhoneNumber { get; set; }

    public string ImageName { get; set; }

    public string Status { get; set; }

    public int Price { get; set; }

    public double Rating { get; set; }

    public string Address { get; set; }

    public IEnumerable<EducationDto> Educations { get; set; }

    public IEnumerable<ExperienceDto> Experiences { get; set; }

    public IEnumerable<ReviewDto> Reviews { get; set; }

    public ProfileCategoryDto ProfileCategory { get; set; }

    public CityDto City { get; set; }
}