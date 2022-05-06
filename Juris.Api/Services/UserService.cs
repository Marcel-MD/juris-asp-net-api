using System.Net;
using AutoMapper;
using Juris.Api.IServices;
using Juris.Common.Dtos.User;
using Juris.Common.Exceptions;
using Juris.Domain.Constants;
using Juris.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Juris.Api.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, RoleManager<Role> roleManager,
        IAuthService authService, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService;
        _mapper = mapper;
    }
    
    public async Task<UserDto> RegisterUser(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.UserName = user.Email;
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded) throw new HttpResponseException(HttpStatusCode.BadRequest, result.Errors.Select(e => e.Description).First());

        if (await _roleManager.RoleExistsAsync(RoleType.User)) await _userManager.AddToRoleAsync(user, RoleType.User);

        user = await _userManager.FindByEmailAsync(dto.Email);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserTokenDto> LoginUser(CreateUserDto dto)
    {
        if (!await _authService.ValidateUser(dto.Email, dto.Password)) throw new HttpResponseException(HttpStatusCode.Unauthorized);

        var user = await _userManager.FindByEmailAsync(dto.Email);
        var roles = await _userManager.GetRolesAsync(user);
        var userTokenDto = _mapper.Map<UserTokenDto>(user);
        userTokenDto.Roles = roles.ToList();
        userTokenDto.Token = await _authService.CreateToken();
        if (user.Profile != null)
            userTokenDto.ProfileId = user.Profile.Id;

        return userTokenDto;
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = _userManager.Users;
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}