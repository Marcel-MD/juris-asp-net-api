namespace Juris.Domain.Entities;

public class Review : BaseEntity
{
    public long ProfileId { get; set; }

    public Profile Profile { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }

    public int Rating { get; set; }

    public DateTime CreationDate { get; set; }
}