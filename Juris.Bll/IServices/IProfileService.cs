using Juris.Common.Dtos.Profile;
using Juris.Common.Parameters;
using Microsoft.AspNetCore.Http;

namespace Juris.Bll.IServices;

public interface IProfileService
{
    /// <summary>
    ///     Gets all profiles by specified parameters.
    /// </summary>
    /// <param name="parameters">Sorting, filtering, paging parameters.</param>
    /// <returns>List of profiles.</returns>
    Task<IEnumerable<ListProfileDto>> GetAllProfiles(ProfileParameters parameters);

    /// <summary>
    ///     Gets profile by id.
    /// </summary>
    /// <param name="id">If of the profile.</param>
    /// <returns>Profile.</returns>
    Task<ProfileDto> GetProfileById(long id);

    /// <summary>
    ///     Creates new profile.
    /// </summary>
    /// <param name="profileDto">Profile Dto.</param>
    /// <param name="userId">Id of the current user.</param>
    /// <returns>Created profile.</returns>
    Task<ListProfileDto> CreateProfile(UpdateProfileDto profileDto, long userId);

    /// <summary>
    ///     Creates an empty profile, with first city and category by default.
    /// </summary>
    /// <param name="userId">Id of the current user.</param>
    /// <returns>Created profile.</returns>
    Task<ListProfileDto> CreateEmptyProfile(long userId);

    /// <summary>
    ///     Updates a profile.
    /// </summary>
    /// <param name="profileDto">Profile dto.</param>
    /// <param name="profileId">Id of the profile.</param>
    /// <param name="userId">Id of the current user.</param>
    /// <returns>Updated profile.</returns>
    Task<ListProfileDto> UpdateProfile(UpdateProfileDto profileDto, long profileId, long userId);

    /// <summary>
    ///     Deletes profile by id.
    /// </summary>
    /// <param name="profileId">Id of the profile.</param>
    /// <param name="userId">Id of the current user.</param>
    Task DeleteProfile(long profileId, long userId);

    /// <summary>
    ///     Uploads new profile image.
    /// </summary>
    /// <param name="image">Profile image.</param>
    /// <param name="profileId">Id of the profile to upload image to.</param>
    /// <param name="userId">Id of the current user.</param>
    /// <returns>Name of the uploaded image.</returns>
    Task<string> UpdateProfileImage(IFormFile image, long profileId, long userId);

    /// <summary>
    ///     Updates profile status.
    /// </summary>
    /// <param name="status">New profile status.</param>
    /// <param name="profileId">Id of the profile.</param>
    Task UpdateProfileStatus(string status, long profileId);
}