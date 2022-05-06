using Juris.Common.Parameters;
using Juris.Domain.Entities;

namespace Juris.Api.IServices;

public interface IProfileService
{
    Task<IEnumerable<Profile>> GetAllProfiles(ProfileParameters parameters);
    Task<Profile> GetProfileById(long id);
    Task<Profile> CreateProfile(Profile profile, long userId);
    Task<Profile> CreateEmptyProfile(long userId);
    Task<Profile> UpdateProfile(Profile profile, long profileId, long userId);
    Task DeleteProfile(long profileId, long userId);
    Task<string> UpdateProfileImage(IFormFile image, long profileId, long userId);
    Task UpdateProfileStatus(string status, long profileId);
}