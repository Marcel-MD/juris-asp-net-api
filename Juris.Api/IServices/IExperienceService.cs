using Juris.Common.Dtos.Experience;
namespace Juris.Api.IServices;

public interface IExperienceService
{
    Task<IEnumerable<ExperienceDto>> GetAllExperience(long profileId);
    Task<ExperienceDto> CreateExperience(CreateExperienceDto experience, long profileId, long userId);
    Task DeleteExperience(long id, long userId);
}