using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.AppointmentRequest;

public class CreateAppointmentRequestDto
{
    [Required]
    [MinLength(3)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string PhoneNumber { get; set; }

    [Required]
    [MinLength(16)]
    public string Description { get; set; }
}