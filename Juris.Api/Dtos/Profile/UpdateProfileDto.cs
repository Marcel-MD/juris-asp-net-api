using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.Profile;

public class UpdateProfileDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(50)]
    public string PhoneNumber { get; set; }

    [Required]
    [MinLength(16)]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required] [Range(0, 900000)] public int Price { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(150)]
    public string Address { get; set; }

    [Required] public long ProfileCategoryId { get; set; }

    [Required] public long CityId { get; set; }
}