using Juris.Common.Dtos.Profile;
using Juris.Bll.IServices;
using Juris.Common.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/profile")]
public class ProfileController : BaseController
{
    private readonly IProfileService _service;

    public ProfileController(IProfileService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProfiles([FromQuery] ProfileParameters parameters)
    {
        var result = await _service.GetAllProfiles(parameters);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfileById(long id)
    {
        var result = await _service.GetProfileById(id);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateProfile(UpdateProfileDto dto)
    {
        var result = await _service.CreateProfile(dto, GetCurrentUserId());
        return Ok(result);
    }

    [Authorize]
    [HttpPost("empty")]
    public async Task<IActionResult> CreateEmptyProfile()
    {
        var result = await _service.CreateEmptyProfile(GetCurrentUserId());
        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile(long id, UpdateProfileDto dto)
    {
        var result = await _service.UpdateProfile(dto, id, GetCurrentUserId());
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(long id)
    {
        await _service.DeleteProfile(id, GetCurrentUserId());
        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id}/image")]
    public async Task<IActionResult> UpdateProfileImage(long id, IFormFile image)
    {
        var result = await _service.UpdateProfileImage(image, id, GetCurrentUserId());
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateProfileStatus(long id, string status)
    {
        await _service.UpdateProfileStatus(status, id);
        return NoContent();
    }
}