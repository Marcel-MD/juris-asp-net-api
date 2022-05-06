using AutoMapper;
using Juris.Common.Dtos.Experience;
using Juris.Api.IServices;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/experience")]
public class ExperienceController : BaseController
{
    private readonly IExperienceService _service;

    public ExperienceController(IExperienceService service)
    {
        _service = service;
    }

    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetExperienceByProfileId(long profileId)
    {
        var result = await _service.GetAllExperience(profileId);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("{profileId}")]
    public async Task<IActionResult> CreateExperience(long profileId, CreateExperienceDto dto)
    {
        var result = await _service.CreateExperience(dto, profileId, GetCurrentUserId());
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExperience(long id)
    {
        await _service.DeleteExperience(id, GetCurrentUserId());
        return NoContent();
    }
}