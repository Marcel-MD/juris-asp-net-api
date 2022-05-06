using Juris.Common.Dtos.City;
using Juris.Common.Dtos.ProfileCategory;

namespace Juris.Common.Dtos.Profile;

public class ListProfileDto
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Description { get; set; }

    public string ImageName { get; set; }

    public string Status { get; set; }

    public int Price { get; set; }

    public double Rating { get; set; }

    public string Address { get; set; }

    public ProfileCategoryDto ProfileCategory { get; set; }

    public CityDto City { get; set; }
}