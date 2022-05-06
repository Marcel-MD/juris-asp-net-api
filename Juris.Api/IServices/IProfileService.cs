using Juris.Common.Dtos.Profile;
using Juris.Common.Parameters;

namespace Juris.Api.IServices;

public interface IProfileService
{
    Task<IEnumerable<ListProfileDto>> GetAllProfiles(ProfileParameters parameters);
    Task<ProfileDto> GetProfileById(long id);
    Task<ListProfileDto> CreateProfile(UpdateProfileDto profileDto, long userId);
    Task<ListProfileDto> CreateEmptyProfile(long userId);
    Task<ListProfileDto> UpdateProfile(UpdateProfileDto profileDto, long profileId, long userId);
    Task DeleteProfile(long profileId, long userId);
    Task<string> UpdateProfileImage(IFormFile image, long profileId, long userId);
    Task UpdateProfileStatus(string status, long profileId);
}