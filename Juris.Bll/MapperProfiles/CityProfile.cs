using Juris.Common.Dtos.City;
using Juris.Domain.Entities;

namespace Juris.Bll.MapperProfiles;

public class CityProfile : AutoMapper.Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>();
        CreateMap<CreateCityDto, City>();
    }
}