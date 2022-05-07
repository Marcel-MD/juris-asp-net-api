using System.Net;
using AutoMapper;
using Juris.Common.Exceptions;
using Juris.Bll.IServices;
using Juris.Common.Dtos.City;
using Juris.Dal.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;

namespace Juris.Bll.Services;

public class CityService : ICityService
{
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CityService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cityRepository = _unitOfWork.CityRepository;
    }

    public async Task<IEnumerable<CityDto>> GetCities()
    {
        var cities = await _cityRepository.GetAll();
        return _mapper.Map<IEnumerable<CityDto>>(cities);
    }

    public async Task<CityDto> CreateCity(CreateCityDto cityDto)
    {
        var cit = await _cityRepository.Get(c => c.Name == cityDto.Name);
        if (cit != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                string.Format(GlobalResource.CityNameExists, cityDto.Name));

        var city = _mapper.Map<City>(cityDto);
        
        await _cityRepository.Insert(city);
        await _unitOfWork.Save();

        return _mapper.Map<CityDto>(city);
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