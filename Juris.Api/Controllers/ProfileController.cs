using AutoMapper;
using Juris.Api.Dtos.Profile;
using Juris.Api.IServices;
using Juris.Api.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile = Juris.Domain.Entities.Profile;

namespace Juris.Api.Controllers;

[Route("api/profile")]
public class ProfileController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IProfileService _service;

    public ProfileController(IProfileService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProfiles([FromQuery] ProfileParameters parameters)
    {
        var result = await _service.GetAllProfiles(parameters);
        var resultDto = _mapper.Map<IEnumerable<ListProfileDto>>(result);
        return Ok(resultDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfileById(long id)
    {
        var result = await _service.GetProfileById(id);
        var resultDto = _mapper.Map<ProfileDto>(result);
        return Ok(resultDto);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateProfile(UpdateProfileDto dto)
    {
        var profile = _mapper.Map<Profile>(dto);
        await _service.CreateProfile(profile, GetCurrentUserId());
        return Ok();
    }

    [Authorize]
    [HttpPost("empty")]
    public async Task<IActionResult> CreateEmptyProfile()
    {
        await _service.CreateEmptyProfile(GetCurrentUserId());
        return Ok();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile(long id, UpdateProfileDto dto)
    {
        var profile = _mapper.Map<Profile>(dto);
        await _service.UpdateProfile(profile, id, GetCurrentUserId());
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(long id)
    {
        await _service.DeleteProfile(id, GetCurrentUserId());
        return Ok();
    }

    [Authorize]
    [HttpPatch("{id}/image")]
    public async Task<IActionResult> UpdateProfileImage(long id, IFormFile image)
    {
        await _service.UpdateProfileImage(image, id, GetCurrentUserId());
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateProfileStatus(long id, string status)
    {
        await _service.UpdateProfileStatus(status, id);
        return Ok();
    }
}