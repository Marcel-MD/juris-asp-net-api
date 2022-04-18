using Juris.Domain.Entities;

namespace Juris.Api.IServices;

public interface ICityService
{
    Task<IEnumerable<City>> GetCities();
    Task<City> CreateCity(City city);
    Task DeleteCity(long id);
}