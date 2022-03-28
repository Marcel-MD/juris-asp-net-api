namespace Juris.Api.IServices;

public interface IAuthService
{
    Task<bool> ValidateUser(string email, string password);
    Task<string> CreateToken();
}