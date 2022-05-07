using AutoMapper;
using Juris.Common.Dtos.Education;
using Juris.Bll.IServices;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/education")]
public class EducationController : BaseController
{
    private readonly IEducationService _service;

    public EducationController(IEducationService service)
    {
        _service = service;
    }

    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetEducationByProfileId(long profileId)
    {
        var result = await _service.GetAllEducation(profileId);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("{profileId}")]
    public async Task<IActionResult> CreateEducation(long profileId, CreateEducationDto dto)
    {
        var result = await _service.CreateEducation(dto, profileId, GetCurrentUserId());
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEducation(long id)
    {
        await _service.DeleteEducation(id, GetCurrentUserId());
        return NoContent();
    }
}