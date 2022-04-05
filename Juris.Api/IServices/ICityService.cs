using Juris.Domain.Entities;

namespace Juris.Api.IServices;

public interface ICityService
{
    Task<IEnumerable<City>> GetCities();
    Task CreateCity(City city);
    Task DeleteCity(long id);
}