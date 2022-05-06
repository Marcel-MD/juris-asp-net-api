using System.Net;
using Juris.Common.Exceptions;
using Juris.Api.IServices;
using Juris.Dal.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;

namespace Juris.Api.Services;

public class CityService : ICityService
{
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _cityRepository = _unitOfWork.CityRepository;
    }

    public async Task<IEnumerable<City>> GetCities()
    {
        return await _cityRepository.GetAll();
    }

    public async Task<City> CreateCity(City city)
    {
        var cit = await _cityRepository.Get(c => c.Name == city.Name);
        if (cit != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                string.Format(GlobalResource.CityNameExists, city.Name));

        await _cityRepository.Insert(city);
        await _unitOfWork.Save();

        return city;
    }

    public async Task DeleteCity(long id)
    {
        var city = await _cityRepository.GetById(id);
        if (city == null)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                string.Format(GlobalResource.CityNotFound, id));

        try
        {
            await _cityRepository.Delete(id);
            await _unitOfWork.Save();
        }
        catch (Exception e)
        {
            throw new HttpResponseException(HttpStatusCode.BadRequest, GlobalResource.CantDeleteResource);
        }
    }
}