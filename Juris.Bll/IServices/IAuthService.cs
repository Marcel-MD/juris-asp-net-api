namespace Juris.Bll.IServices;

public interface IAuthService
{
    /// <summary>
    ///     Checks if user credentials are valid.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="password">User password.</param>
    /// <returns>True if credentials are valid, false otherwise.</returns>
    Task<bool> ValidateUser(string email, string password);

    /// <summary>
    ///     Creates a json web token for current user.
    /// </summary>
    /// <returns>Token.</returns>
    Task<string> CreateToken();
}