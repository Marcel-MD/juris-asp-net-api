using Juris.Api.Dtos.Profile;
using Juris.Models.Entities;

namespace Juris.Api.MapperProfiles;

public class ProfileProfile : AutoMapper.Profile
{
    public ProfileProfile()
    {
        CreateMap<Profile, ProfileDto>();
        CreateMap<Profile, UpdateProfileDto>().ReverseMap();
    }
}