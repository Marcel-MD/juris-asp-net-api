using Juris.Common.Dtos.User;
using Juris.Bll.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        this._service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto dto)
    {
        var result = await _service.RegisterUser(dto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(CreateUserDto dto)
    {
        var result = await _service.LoginUser(dto);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _service.GetUsers();
        return Ok(result);
    }
}