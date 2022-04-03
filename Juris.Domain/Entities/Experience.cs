namespace Juris.Domain.Entities;

public class Experience : BaseEntity
{
    public long ProfileId { get; set; }

    public virtual Profile Profile { get; set; }

    public string Company { get; set; }

    public string Position { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}