using Juris.Domain.Entities;

namespace Juris.Api.IServices;

public interface IEducationService
{
    Task<IEnumerable<Education>> GetAllEducation(long profileId);
    Task<Education> CreateEducation(Education education, long profileId, long userId);
    Task DeleteEducation(long id, long userId);
}