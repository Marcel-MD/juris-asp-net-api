using AutoMapper;
using Juris.Api.Dtos.City;
using Juris.Api.Dtos.Profile;
using Juris.Api.Dtos.ProfileCategory;
using Juris.Api.Parameters;
using Juris.Api.Services;
using Juris.Models.Entities;
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

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateProfileStatus(long id, string status)
    {
        await _service.UpdateProfileStatus(status, id);
        return Ok();
    }

    [HttpGet("city")]
    public async Task<IActionResult> GetCities()
    {
        var result = await _service.GetCities();
        var resultDto = _mapper.Map<IEnumerable<CityDto>>(result);
        return Ok(resultDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("city")]
    public async Task<IActionResult> CreateCity(CreateCityDto dto)
    {
        var city = _mapper.Map<City>(dto);
        await _service.CreateCity(city);
        return Ok();
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _service.GetProfileCategories();
        var resultDto = _mapper.Map<IEnumerable<ProfileCategoryDto>>(result);
        return Ok(resultDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("category")]
    public async Task<IActionResult> CreateCategory(CreateProfileCategoryDto dto)
    {
        var category = _mapper.Map<ProfileCategory>(dto);
        await _service.CreateProfileCategory(category);
        return Ok();
    }
}