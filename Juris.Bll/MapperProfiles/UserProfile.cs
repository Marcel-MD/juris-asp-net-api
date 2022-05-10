using Juris.Common.Dtos.User;
using Juris.Domain.Identity;

namespace Juris.Bll.MapperProfiles;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserTokenDto>();
        CreateMap<CreateUserDto, User>();
    }
}