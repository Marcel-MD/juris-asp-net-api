using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.Education;

public class CreateEducationDto
{
    [Required]
    public string Institution { get; set; }

    [Required]
    public string Speciality { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}