using AutoMapper;
using Juris.Common.Dtos.Education;
using Juris.Api.IServices;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/education")]
public class EducationController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IEducationService _service;

    public EducationController(IEducationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetEducationByProfileId(long profileId)
    {
        var result = await _service.GetAllEducation(profileId);
        var resultDto = _mapper.Map<IEnumerable<EducationDto>>(result);
        return Ok(resultDto);
    }

    [Authorize]
    [HttpPost("{profileId}")]
    public async Task<IActionResult> CreateEducation(long profileId, CreateEducationDto dto)
    {
        var education = _mapper.Map<Education>(dto);
        education = await _service.CreateEducation(education, profileId, GetCurrentUserId());
        var educationDto = _mapper.Map<EducationDto>(education);
        return Ok(educationDto);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEducation(long id)
    {
        await _service.DeleteEducation(id, GetCurrentUserId());
        return NoContent();
    }
}