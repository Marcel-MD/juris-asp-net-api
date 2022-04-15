using AutoMapper;
using Juris.Api.Dtos.AppointmentRequest;
using Juris.Api.IServices;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/appointment")]
public class AppointmentRequestController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IAppointmentRequestService _service;

    public AppointmentRequestController(IAppointmentRequestService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAppointmentRequestsByUserId(long userId)
    {
        if (userId != GetCurrentUserId()) return Unauthorized();

        var result = await _service.GetAllRequests(userId);
        var resultDto = _mapper.Map<IEnumerable<AppointmentRequestDto>>(result);
        return Ok(resultDto);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateAppointmentRequest(long userId, CreateAppointmentRequestDto dto)
    {
        var appointmentRequest = _mapper.Map<AppointmentRequest>(dto);
        appointmentRequest = await _service.CreateRequest(appointmentRequest, userId);
        var appointmentDto = _mapper.Map<AppointmentRequestDto>(appointmentRequest);
        return Ok(appointmentDto);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointmentRequest(long id)
    {
        await _service.DeleteRequest(id, GetCurrentUserId());
        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> UpdateAppointmentRequestStatus(long id, string status)
    {
        await _service.UpdateRequestStatus(status, id, GetCurrentUserId());
        return NoContent();
    }
}