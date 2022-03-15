using System.Net;
using Juris.Api.Exceptions;
using Juris.Data.Repositories;
using Juris.Models.Constants;
using Juris.Models.Entities;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Juris.Api.Services;

public class ProfileService : IProfileService
{
    private readonly IGenericRepository<ProfileCategory> _categoryRepository;
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public ProfileService(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _profileRepository = unitOfWork.ProfileRepository;
        _cityRepository = _unitOfWork.CityRepository;
        _categoryRepository = _unitOfWork.ProfileCategoryRepository;
        _userManager = userManager;
    }

    public async Task<IList<Profile>> GetAllProfiles()
    {
        var response = await _profileRepository.GetAll();

        return response;
    }

    public async Task<Profile> GetProfileById(long id)
    {
        return await _profileRepository.GetById(id);
    }

    public async Task CreateProfile(Profile profile, long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={userId} not found");

        var existingProfile = await _profileRepository.Get(p => p.UserId == userId);
        if (existingProfile != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest, $"User with id={userId} already has a profile");

        profile.UserId = userId;

        await _profileRepository.Insert(profile);
        await _unitOfWork.Save();
    }

    public async Task CreateEmptyProfile(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={userId} not found");

        var existingProfile = await _profileRepository.Get(p => p.UserId == userId);
        if (existingProfile != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest, $"User with id={userId} already has a profile");

        var profile = new Profile
        {
            UserId = userId,
            FirstName = "",
            LastName = "",
            Description = "",
            PhoneNumber = "",
            Price = 0,
            ProfileCategoryId = 1,
            Status = ProfileStatus.Unapproved,
            Address = "",
            CityId = 1
        };

        await _profileRepository.Insert(profile);
        await _unitOfWork.Save();
    }

    public async Task UpdateProfile(Profile profile, long profileId, long userId)
    {
        var existingProfile = await _profileRepository.GetById(profileId);
        if (existingProfile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={profileId} not found");

        if (existingProfile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                $"Unauthorized to update profile id={profileId}");

        existingProfile.FirstName = profile.FirstName;
        existingProfile.LastName = profile.LastName;
        existingProfile.Description = profile.Description;
        existingProfile.PhoneNumber = profile.PhoneNumber;
        existingProfile.Price = profile.Price;
        existingProfile.ProfileCategoryId = profile.ProfileCategoryId;
        existingProfile.Address = profile.Address;
        existingProfile.CityId = profile.CityId;

        _profileRepository.Update(existingProfile);
        await _unitOfWork.Save();
    }

    public async Task DeleteProfile(long profileId, long userId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={profileId} not found");

        if (profile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                $"Unauthorized to update profile id={profileId}");

        await _profileRepository.Delete(profileId);
        await _unitOfWork.Save();
    }

    public async Task UpdateProfileStatus(string status, long profileId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={profileId} not found");

        profile.Status = status;

        _profileRepository.Update(profile);
        await _unitOfWork.Save();
    }
}