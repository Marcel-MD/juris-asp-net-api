using System.ComponentModel.DataAnnotations;

namespace Juris.Common.Dtos.City;

public class CreateCityDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; }
}