using Juris.Api.Dtos.AppointmentRequest;
using Juris.Domain.Entities;

namespace Juris.Api.MapperProfiles;

public class AppointmentRequestProfile : AutoMapper.Profile
{
    public AppointmentRequestProfile()
    {
        CreateMap<AppointmentRequest, AppointmentRequestDto>();
        CreateMap<CreateAppointmentRequestDto, AppointmentRequest>();
    }
}