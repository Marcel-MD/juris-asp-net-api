using Juris.Common.Dtos.Education;

namespace Juris.Bll.IServices;

public interface IEducationService
{
    /// <summary>
    ///     Gets all educations by profile id.
    /// </summary>
    /// <param name="profileId">Profile id.</param>
    /// <returns>List of educations.</returns>
    Task<IEnumerable<EducationDto>> GetAllEducation(long profileId);

    /// <summary>
    ///     Creates a new education for a profile.
    /// </summary>
    /// <param name="education">Education dto.</param>
    /// <param name="profileId">Id of the profile.</param>
    /// <param name="userId">Id of the current user.</param>
    /// <returns>Created education.</returns>
    Task<EducationDto> CreateEducation(CreateEducationDto education, long profileId, long userId);

    /// <summary>
    ///     Deletes education by id.
    /// </summary>
    /// <param name="id">Id of the education.</param>
    /// <param name="userId">Id of the current user.</param>
    Task DeleteEducation(long id, long userId);
}