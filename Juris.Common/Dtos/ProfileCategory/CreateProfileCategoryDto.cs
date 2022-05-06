using System.ComponentModel.DataAnnotations;

namespace Juris.Common.Dtos.ProfileCategory;

public class CreateProfileCategoryDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Category { get; set; }
}