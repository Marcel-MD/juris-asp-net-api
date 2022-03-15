using Juris.Api.Dtos.ProfileCategory;
using Juris.Models.Entities;

namespace Juris.Api.MapperProfiles;

public class ProfileCategoryProfile : AutoMapper.Profile
{
    public ProfileCategoryProfile()
    {
        CreateMap<ProfileCategory, ProfileCategoryDto>();
        CreateMap<CreateProfileCategoryDto, ProfileCategory>();
    }
}