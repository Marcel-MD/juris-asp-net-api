using AutoMapper;
using Juris.Api.Dtos.AppointmentRequest;
using Juris.Api.Services;
using Juris.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/appointment")]
[ApiController]
public class AppointmentRequestController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAppointmentRequestService _service;

    public AppointmentRequestController(IAppointmentRequestService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAppointmentRequestsByUserId(long userId)
    {
        var result = await _service.GetAllRequests(userId);
        var resultDto = _mapper.Map<IList<AppointmentRequestDto>>(result);
        return Ok(resultDto);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> PostAppointmentRequest(long userId, CreateAppointmentRequestDto dto)
    {
        var appointmentRequest = _mapper.Map<AppointmentRequest>(dto);
        await _service.CreateRequest(appointmentRequest, userId);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointmentRequest(long id)
    {
        await _service.DeleteRequest(id);
        return Ok();
    }

    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateAppointmentRequestStatus(long id, string status)
    {
        await _service.UpdateRequestStatus(status, id);
        return Ok();
    }
}