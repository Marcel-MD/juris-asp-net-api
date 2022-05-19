using Juris.Common.Dtos.AppointmentRequest;

namespace Juris.Bll.IServices;

public interface IAppointmentRequestService
{
    /// <summary>
    ///     Gets all appointment requests by user id.
    /// </summary>
    /// <param name="userId">Id of user.</param>
    /// <returns>User's appointment requests.</returns>
    Task<IEnumerable<AppointmentRequestDto>> GetAllRequests(long userId);

    /// <summary>
    ///     Creates a new appointment request.
    /// </summary>
    /// <param name="request">Appointment request dto.</param>
    /// <param name="userId">Id of user.</param>
    /// <returns>Created appointment request dto.</returns>
    Task<AppointmentRequestDto> CreateRequest(CreateAppointmentRequestDto request, long userId);

    /// <summary>
    ///     Deletes appointment request by it's id.
    /// </summary>
    /// <param name="requestId">Appointment request id.</param>
    /// <param name="userId">Id of the current user.</param>
    Task DeleteRequest(long requestId, long userId);

    /// <summary>
    ///     Updates the status of the appointment request.
    /// </summary>
    /// <param name="status">Appointment request new status.</param>
    /// <param name="requestId">Appointment request id.</param>
    /// <param name="userId">Id of the current user.</param>
    /// <returns></returns>
    Task UpdateRequestStatus(string status, long requestId, long userId);
}