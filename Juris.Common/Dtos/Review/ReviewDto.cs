namespace Juris.Common.Dtos.Review;

public class ReviewDto
{
    public long Id { get; set; }
    
    public long ProfileId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Description { get; set; }

    public int Rating { get; set; }

    public DateTime CreationDate { get; set; }
}