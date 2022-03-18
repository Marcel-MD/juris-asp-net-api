using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.ProfileCategory;

public class CreateProfileCategoryDto
{
    [Required]
    [MinLength(3)]
    public string Category { get; set; }
}