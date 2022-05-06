namespace Juris.Common.Parameters;

public class ProfileParameters : PagingParameters
{
    public int? CategoryId { get; set; } = null;
    public int? CityId { get; set; } = null;
    public string Status { get; set; } = null;
    public string SortBy { get; set; } = null;
}