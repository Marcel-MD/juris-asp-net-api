namespace Juris.Api.Dtos.Education;

public class EducationDto
{
    public long Id { get; set; }
    
    public long ProfileId { get; set; }

    public string Institution { get; set; }

    public string Speciality { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}