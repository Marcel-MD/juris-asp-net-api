using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.Experience;

public class CreateExperienceDto
{
    [Required]
    public string Company { get; set; }

    [Required]
    public string Position { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}