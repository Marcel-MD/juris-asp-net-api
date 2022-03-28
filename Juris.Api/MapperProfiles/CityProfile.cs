using Juris.Api.Dtos.City;
using Juris.Domain.Entities;

namespace Juris.Api.MapperProfiles;

public class CityProfile : AutoMapper.Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>();
        CreateMap<CreateCityDto, City>();
    }
}