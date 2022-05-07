using Juris.Common.Dtos.User;

namespace Juris.Bll.IServices;

public interface IUserService
{
    Task<UserDto> RegisterUser(CreateUserDto user);
    Task<UserTokenDto> LoginUser(CreateUserDto user);
    Task<IEnumerable<UserDto>> GetUsers();
}