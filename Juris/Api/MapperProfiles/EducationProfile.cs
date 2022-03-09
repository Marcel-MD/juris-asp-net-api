using Juris.Api.Dtos.Education;
using Juris.Models.Entities;

namespace Juris.Api.MapperProfiles;

public class EducationProfile : AutoMapper.Profile
{
    public EducationProfile()
    {
        CreateMap<Education, EducationDto>();
        CreateMap<CreateEducationDto, Education>();
    }
}