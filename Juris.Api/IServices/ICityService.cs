using Juris.Common.Dtos.City;

namespace Juris.Api.IServices;

public interface ICityService
{
    Task<IEnumerable<CityDto>> GetCities();
    Task<CityDto> CreateCity(CreateCityDto city);
    Task DeleteCity(long id);
}