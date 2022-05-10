using Juris.Common.Dtos.Experience;
using Juris.Domain.Entities;

namespace Juris.Bll.MapperProfiles;

public class ExperienceProfile : AutoMapper.Profile
{
    public ExperienceProfile()
    {
        CreateMap<Experience, ExperienceDto>();
        CreateMap<CreateExperienceDto, Experience>();
    }
}