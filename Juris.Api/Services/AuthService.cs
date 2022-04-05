using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Juris.Api.Configuration;
using Juris.Api.IServices;
using Juris.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Juris.Api.Services;

public class AuthService : IAuthService
{
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<User> _userManager;
    private User _user;

    public AuthService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;

        _jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
    }

    public async Task<bool> ValidateUser(string email, string password)
    {
        _user = await _userManager.FindByNameAsync(email);
        var validPassword = await _userManager.CheckPasswordAsync(_user, password);
        return _user != null && validPassword;
    }

    public async Task<string> CreateToken()
    {
        var key = _jwtOptions.GetSymmetricSecurityKey();
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = await GetClaims();
        var token = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, _user.UserName),
            new(ClaimTypes.NameIdentifier, _user.Id.ToString())
        };

        var roles = await _userManager.GetRolesAsync(_user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.Now.AddHours(_jwtOptions.TokenLifetime),
            signingCredentials: signingCredentials
        );

        return token;
    }
}