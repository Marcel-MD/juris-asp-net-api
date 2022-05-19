using Juris.Common.Dtos.User;

namespace Juris.Bll.IServices;

public interface IUserService
{
    /// <summary>
    ///     Creates a new user.
    /// </summary>
    /// <param name="user">User dto.</param>
    /// <returns>Created user dto.</returns>
    Task<UserDto> RegisterUser(CreateUserDto user);

    /// <summary>
    ///     Logs in the user and returns a token.
    /// </summary>
    /// <param name="user">User dto.</param>
    /// <returns>User dto with token.</returns>
    Task<UserTokenDto> LoginUser(CreateUserDto user);

    /// <summary>
    ///     Returns the list of existing users. For testing purpose.
    /// </summary>
    /// <returns>List of users.</returns>
    Task<IEnumerable<UserDto>> GetUsers();
}