namespace Juris.Api.Dtos.AppointmentRequest;

public class AppointmentRequestDto
{
    public long Id { get; set; }
    
    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public DateTime CreationDate { get; set; }
}