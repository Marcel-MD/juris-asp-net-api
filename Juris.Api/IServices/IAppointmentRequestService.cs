using Juris.Common.Dtos.AppointmentRequest;

namespace Juris.Api.IServices;

public interface IAppointmentRequestService
{
    Task<IEnumerable<AppointmentRequestDto>> GetAllRequests(long userId);
    Task<AppointmentRequestDto> CreateRequest(CreateAppointmentRequestDto request, long userId);
    Task DeleteRequest(long requestId, long userId);
    Task UpdateRequestStatus(string status, long requestId, long userId);
}