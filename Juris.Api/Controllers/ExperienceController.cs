using AutoMapper;
using Juris.Api.Dtos.Experience;
using Juris.Api.IServices;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/experience")]
public class ExperienceController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IExperienceService _service;

    public ExperienceController(IExperienceService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetExperienceByProfileId(long profileId)
    {
        var result = await _service.GetAllExperience(profileId);
        var resultDto = _mapper.Map<IEnumerable<ExperienceDto>>(result);
        return Ok(resultDto);
    }

    [Authorize]
    [HttpPost("{profileId}")]
    public async Task<IActionResult> CreateExperience(long profileId, CreateExperienceDto dto)
    {
        var experience = _mapper.Map<Experience>(dto);
        experience = await _service.CreateExperience(experience, profileId, GetCurrentUserId());
        var experienceDto = _mapper.Map<ExperienceDto>(experience);
        return Ok(experienceDto);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExperience(long id)
    {
        await _service.DeleteExperience(id, GetCurrentUserId());
        return NoContent();
    }
}