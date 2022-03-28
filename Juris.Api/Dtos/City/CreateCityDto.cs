using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.City;

public class CreateCityDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; }
}