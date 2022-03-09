using Juris.Api.Dtos.User;
using Juris.Models.Identity;

namespace Juris.Api.MapperProfiles;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserTokenDto>();
        CreateMap<CreateUserDto, User>();
    }
}