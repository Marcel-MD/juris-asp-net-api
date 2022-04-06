using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.AppointmentRequest;

public class CreateAppointmentRequestDto
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
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(50)]
    public string PhoneNumber { get; set; }

    [Required]
    [MinLength(16)]
    [MaxLength(500)]
    public string Description { get; set; }
}