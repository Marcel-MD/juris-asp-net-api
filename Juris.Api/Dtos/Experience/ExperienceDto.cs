namespace Juris.Api.Dtos.Experience;

public class ExperienceDto
{
    public long Id { get; set; }
    
    public long ProfileId { get; set; }

    public string Company { get; set; }

    public string Position { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}