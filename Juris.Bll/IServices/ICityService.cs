using Juris.Common.Dtos.City;

namespace Juris.Bll.IServices;

public interface ICityService
{
    Task<IEnumerable<CityDto>> GetCities();
    Task<CityDto> CreateCity(CreateCityDto city);
    Task DeleteCity(long id);
}