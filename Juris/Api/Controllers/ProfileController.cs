using AutoMapper;
using Juris.Api.Dtos.Profile;
using Juris.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile = Juris.Models.Entities.Profile;

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
    public async Task<IActionResult> GetAllProfiles()
    {
        var result = await _service.GetAllProfiles();
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
    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateProfileStatus(long id, string status)
    {
        await _service.UpdateProfileStatus(status, id);
        return Ok();
    }
}