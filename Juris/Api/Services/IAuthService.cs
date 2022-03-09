using Juris.Api.Dtos.User;

namespace Juris.Api.Services;

public interface IAuthService
{
    Task<bool> ValidateUser(string email, string password);
    Task<string> CreateToken();
}