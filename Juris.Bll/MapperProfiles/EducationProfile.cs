using Juris.Common.Dtos.Education;
using Juris.Domain.Entities;

namespace Juris.Bll.MapperProfiles;

public class EducationProfile : AutoMapper.Profile
{
    public EducationProfile()
    {
        CreateMap<Education, EducationDto>();
        CreateMap<CreateEducationDto, Education>();
    }
}