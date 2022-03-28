using Juris.Domain.Entities;

namespace Juris.Api.Services;

public interface IExperienceService
{
    Task<IEnumerable<Experience>> GetAllExperience(long profileId);
    Task CreateExperience(Experience experience, long profileId, long userId);
    Task DeleteExperience(long id, long userId);
}