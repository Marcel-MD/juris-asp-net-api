using AutoMapper;
using Juris.Api.Dtos.Education;
using Juris.Api.Services;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/education")]
public class EducationController : BaseController
{
    private readonly IEducationService _service;
    private readonly IMapper _mapper;

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
        await _service.CreateEducation(education, profileId, GetCurrentUserId());
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEducation(long id)
    {
        await _service.DeleteEducation(id, GetCurrentUserId());
        return Ok();
    }
}