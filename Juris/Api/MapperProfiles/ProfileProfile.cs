using AutoMapper;
using Juris.Api.Dtos.Profile;

namespace Juris.Api.MapperProfiles;

public class ProfileProfile : Profile
{
    public ProfileProfile()
    {
        CreateMap<Models.Entities.Profile, ProfileDto>();
        CreateMap<Models.Entities.Profile, ListProfileDto>();
        CreateMap<UpdateProfileDto, Models.Entities.Profile>();
    }
}