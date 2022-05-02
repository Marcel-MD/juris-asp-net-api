using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.Education;

public class CreateEducationDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Institution { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Speciality { get; set; }

    [Required] public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}