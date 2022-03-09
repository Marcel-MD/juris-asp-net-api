using Juris.Models.Entities;

namespace Juris.Api.Services;

public interface IAppointmentRequestService
{
    Task<IList<AppointmentRequest>> GetAllRequests(long userId);
    Task CreateRequest(AppointmentRequest request, long userId);
    Task DeleteRequest(long requestId, long userId);
    Task UpdateRequestStatus(string status, long requestId, long userId);
}