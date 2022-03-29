namespace Juris.Domain.Entities;

public class Education : BaseEntity
{
    public long ProfileId { get; set; }

    public Profile Profile { get; set; }

    public string Institution { get; set; }

    public string Speciality { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}