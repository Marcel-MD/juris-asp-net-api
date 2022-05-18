using Juris.Common.Dtos.Experience;

namespace Juris.Bll.IServices;

public interface IExperienceService
{
    /// <summary>
    ///     Gets all experiences by profile id.
    /// </summary>
    /// <param name="profileId">Profile id.</param>
    /// <returns>List of experiences.</returns>
    Task<IEnumerable<ExperienceDto>> GetAllExperience(long profileId);

    /// <summary>
    ///     Creates a new experience for a profile.
    /// </summary>
    /// <param name="experience">Experience dto.</param>
    /// <param name="profileId">Id of the profile.</param>
    /// <param name="userId">Id of the current user.</param>
    /// <returns>Created experience.</returns>
    Task<ExperienceDto> CreateExperience(CreateExperienceDto experience, long profileId, long userId);

    /// <summary>
    ///     Deletes experience by id.
    /// </summary>
    /// <param name="id">Id of the experience.</param>
    /// <param name="userId">Id of the current user.</param>
    Task DeleteExperience(long id, long userId);
}