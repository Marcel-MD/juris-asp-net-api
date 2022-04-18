using AutoMapper;
using Juris.Api.Dtos.User;
using Juris.Api.IServices;
using Juris.Domain.Constants;
using Juris.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager, RoleManager<Role> roleManager,
        IAuthService authService, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.UserName = user.Email;
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded) return BadRequest(new {errors = result.Errors.Select(e => e.Description)});

        if (await _roleManager.RoleExistsAsync(RoleType.User)) await _userManager.AddToRoleAsync(user, RoleType.User);

        user = await _userManager.FindByEmailAsync(dto.Email);
        var userDto = _mapper.Map<UserDto>(user);
        return Ok(userDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(CreateUserDto dto)
    {
        if (!await _authService.ValidateUser(dto.Email, dto.Password)) return Unauthorized();

        var user = await _userManager.FindByEmailAsync(dto.Email);
        var roles = await _userManager.GetRolesAsync(user);
        var userTokenDto = _mapper.Map<UserTokenDto>(user);
        userTokenDto.Roles = roles.ToList();
        userTokenDto.Token = await _authService.CreateToken();

        return Ok(userTokenDto);
    }
}