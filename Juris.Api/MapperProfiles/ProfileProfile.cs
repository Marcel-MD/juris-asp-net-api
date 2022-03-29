using AutoMapper;
using Juris.Api.Dtos.Profile;

namespace Juris.Api.MapperProfiles;

public class ProfileProfile : Profile
{
    public ProfileProfile()
    {
        CreateMap<Domain.Entities.Profile, ProfileDto>();
        CreateMap<Domain.Entities.Profile, ListProfileDto>();
        CreateMap<UpdateProfileDto, Domain.Entities.Profile>();
    }
}