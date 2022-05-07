using Juris.Common.Dtos.Education;

namespace Juris.Bll.IServices;

public interface IEducationService
{
    Task<IEnumerable<EducationDto>> GetAllEducation(long profileId);
    Task<EducationDto> CreateEducation(CreateEducationDto education, long profileId, long userId);
    Task DeleteEducation(long id, long userId);
}