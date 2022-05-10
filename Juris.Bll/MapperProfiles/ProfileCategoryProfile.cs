using Juris.Common.Dtos.ProfileCategory;
using Juris.Domain.Entities;

namespace Juris.Bll.MapperProfiles;

public class ProfileCategoryProfile : AutoMapper.Profile
{
    public ProfileCategoryProfile()
    {
        CreateMap<ProfileCategory, ProfileCategoryDto>();
        CreateMap<CreateProfileCategoryDto, ProfileCategory>();
    }
}