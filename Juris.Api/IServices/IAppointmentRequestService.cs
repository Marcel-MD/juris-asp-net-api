using Juris.Domain.Entities;

namespace Juris.Api.IServices;

public interface IAppointmentRequestService
{
    Task<IEnumerable<AppointmentRequest>> GetAllRequests(long userId);
    Task<AppointmentRequest> CreateRequest(AppointmentRequest request, long userId);
    Task DeleteRequest(long requestId, long userId);
    Task UpdateRequestStatus(string status, long requestId, long userId);
}