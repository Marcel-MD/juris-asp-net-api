using System.Net;
using Juris.Api.Exceptions;
using Juris.Api.IServices;
using Juris.Data.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;

namespace Juris.Api.Services;

public class CityService : ICityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<City> _cityRepository;

    public CityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _cityRepository = _unitOfWork.CityRepository;
    }
    
    public async Task<IEnumerable<City>> GetCities()
    {
        return await _cityRepository.GetAll();
    }

    public async Task CreateCity(City city)
    {
        var cit = await _cityRepository.Get(c => c.Name == city.Name);
        if (cit != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                string.Format(GlobalResource.CityNameExists, city.Name));

        await _cityRepository.Insert(city);
        await _unitOfWork.Save();
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