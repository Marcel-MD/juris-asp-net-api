using Juris.Common.Dtos.City;

namespace Juris.Bll.IServices;

public interface ICityService
{
    /// <summary>
    ///     Gets all the cities.
    /// </summary>
    /// <returns>List of cities.</returns>
    Task<IEnumerable<CityDto>> GetCities();

    /// <summary>
    ///     Creates a new city.
    /// </summary>
    /// <param name="city">City dto.</param>
    /// <returns>Created city.</returns>
    Task<CityDto> CreateCity(CreateCityDto city);

    /// <summary>
    ///     Deletes city by id. Works only if city is not used in any profile.
    /// </summary>
    /// <param name="id">City id.</param>
    Task DeleteCity(long id);
}