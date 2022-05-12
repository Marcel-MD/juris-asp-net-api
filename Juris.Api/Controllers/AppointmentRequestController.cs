using Juris.Bll.IServices;
using Juris.Common.Dtos.AppointmentRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/appointment")]
public class AppointmentRequestController : BaseController
{
    private readonly IAppointmentRequestService _service;

    public AppointmentRequestController(IAppointmentRequestService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAppointmentRequestsByUserId(long userId)
    {
        if (userId != GetCurrentUserId()) return Unauthorized();

        var result = await _service.GetAllRequests(userId);
        return Ok(result);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateAppointmentRequest(long userId, CreateAppointmentRequestDto dto)
    {
        var result = await _service.CreateRequest(dto, userId);
        return Ok(result);
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