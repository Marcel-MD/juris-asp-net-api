using Juris.Api.Dtos.Experience;
using Juris.Models.Entities;

namespace Juris.Api.MapperProfiles;

public class ExperienceProfile : AutoMapper.Profile
{
    public ExperienceProfile()
    {
        CreateMap<Experience, ExperienceDto>();
        CreateMap<CreateExperienceDto, Experience>();
    }
}