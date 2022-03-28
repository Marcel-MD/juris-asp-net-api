using Juris.Api.Parameters;
using Juris.Domain.Entities;

namespace Juris.Api.Services;

public interface IProfileService
{
    Task<IEnumerable<Profile>> GetAllProfiles(ProfileParameters parameters);
    Task<Profile> GetProfileById(long id);
    Task CreateProfile(Profile profile, long userId);
    Task CreateEmptyProfile(long userId);
    Task UpdateProfile(Profile profile, long profileId, long userId);
    Task DeleteProfile(long profileId, long userId);
    Task UpdateProfileStatus(string status, long profileId);
    Task<IEnumerable<City>> GetCities();
    Task CreateCity(City city);
    Task<IEnumerable<ProfileCategory>> GetProfileCategories();
    Task CreateProfileCategory(ProfileCategory category);
}