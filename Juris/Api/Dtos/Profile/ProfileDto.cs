namespace Juris.Api.Dtos.Profile;

public class ProfileDto
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Description { get; set; }

    public string ProfileType { get; set; }

    public string Status { get; set; }

    public int Price { get; set; }

    public double Rating { get; set; }

    public string City { get; set; }

    public string Address { get; set; }
}