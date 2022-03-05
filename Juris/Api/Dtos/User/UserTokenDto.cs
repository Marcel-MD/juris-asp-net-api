namespace Juris.Api.Dtos.User;

public class UserTokenDto
{
    public long Id { get; set; }
    
    public string Email { get; set; }
    
    public string Role { get; set; }
    
    public string Token { get; set; }
}