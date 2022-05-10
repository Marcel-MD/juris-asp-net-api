namespace Juris.Common.Dtos.User;

public class UserTokenDto
{
    public long Id { get; set; }

    public string Email { get; set; }

    public List<string> Roles { get; set; }

    public string Token { get; set; }
    
    public long ProfileId { get; set; }
}