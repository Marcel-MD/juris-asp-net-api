using Juris.Models.Entities;

namespace Juris.Api.Services;

public interface IProfileService
{
    Task<IList<Profile>> GetAllProfiles();
    Task<Profile> GetProfileById(long id);
    Task CreateProfile(Profile profile, long userId);
    Task CreateEmptyProfile(long userId);
    Task UpdateProfile(Profile profile, long profileId, long userId);
    Task DeleteProfile(long profileId, long userId);
    Task UpdateProfileStatus(string status, long profileId);
}