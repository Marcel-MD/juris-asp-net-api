using Juris.Common.Dtos.AppointmentRequest;
using Juris.Domain.Entities;

namespace Juris.Bll.MapperProfiles;

public class AppointmentRequestProfile : AutoMapper.Profile
{
    public AppointmentRequestProfile()
    {
        CreateMap<AppointmentRequest, AppointmentRequestDto>();
        CreateMap<CreateAppointmentRequestDto, AppointmentRequest>();
    }
}