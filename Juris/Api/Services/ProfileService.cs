using System.Net;
using Juris.Api.Exceptions;
using Juris.Data.Repositories;
using Juris.Models.Constants;
using Juris.Models.Entities;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Api.Services;

public class ProfileService : IProfileService
{
    private readonly IGenericRepository<ProfileCategory> _categoryRepository;
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IMailService _mailService;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public ProfileService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMailService mailService)
    {
        _unitOfWork = unitOfWork;
        _profileRepository = unitOfWork.ProfileRepository;
        _cityRepository = _unitOfWork.CityRepository;
        _categoryRepository = _unitOfWork.ProfileCategoryRepository;
        _userManager = userManager;
        _mailService = mailService;
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

        var user = await _userManager.FindByIdAsync(profile.UserId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={profile.UserId} not found");

        profile.Status = status;

        _profileRepository.Update(profile);
        await _unitOfWork.Save();
        try
        {
            await _mailService.SendAsync(
                user.Email,
                "Profile Status Updated",
                $"Status of your profile on Juris has changed to <strong>{status}</strong>.");
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw new HttpResponseException(HttpStatusCode.FailedDependency,
                "Could not send notification email to user");
        }
    }

    public async Task<IEnumerable<Profile>> GetAllProfiles()
    {
        var response = await _profileRepository.GetAll(
            null, null,
            p => p.City, p => p.ProfileCategory);

        return response;
    }

    public async Task<Profile> GetProfileById(long id)
    {
        var profile = await _profileRepository.Get(p => p.Id == id,
            p => p.City, p => p.ProfileCategory, p => p.Educations, p => p.Experiences, p => p.Reviews);

        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={id} not found");

        return profile;
    }

    public async Task CreateProfile(Profile profile, long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={userId} not found");

        var existingProfile = await _profileRepository.Get(p => p.UserId == userId);
        if (existingProfile != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest, $"User with id={userId} already has a profile");

        var city = await _cityRepository.GetById(profile.CityId);
        if (city == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"City with id={profile.CityId} not found");

        var category = await _categoryRepository.GetById(profile.ProfileCategoryId);
        if (category == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                $"Profile Category with id={profile.ProfileCategoryId} not found");

        profile.UserId = userId;

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

        var city = await _cityRepository.GetById(profile.CityId);
        if (city == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"City with id={profile.CityId} not found");

        var category = await _categoryRepository.GetById(profile.ProfileCategoryId);
        if (category == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                $"Profile Category with id={profile.ProfileCategoryId} not found");

        existingProfile.FirstName = profile.FirstName;
        existingProfile.LastName = profile.LastName;
        existingProfile.Description = profile.Description;
        existingProfile.PhoneNumber = profile.PhoneNumber;
        existingProfile.Price = profile.Price;
        existingProfile.ProfileCategoryId = profile.ProfileCategoryId;
        existingProfile.Address = profile.Address;
        existingProfile.CityId = profile.CityId;
        existingProfile.Status = ProfileStatus.Unapproved;

        _profileRepository.Update(existingProfile);
        await _unitOfWork.Save();
    }

    public async Task<IEnumerable<City>> GetCities()
    {
        return await _cityRepository.GetAll();
    }

    public async Task CreateCity(City city)
    {
        var cit = await _cityRepository.Get(c => c.Name == city.Name);
        if (cit != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest, $"City with name={city.Name} already exists");

        await _cityRepository.Insert(city);
        await _unitOfWork.Save();
    }

    public async Task<IEnumerable<ProfileCategory>> GetProfileCategories()
    {
        return await _categoryRepository.GetAll();
    }

    public async Task CreateProfileCategory(ProfileCategory category)
    {
        var cat = await _categoryRepository.Get(c => c.Category == category.Category);
        if (cat != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                $"Category with name={category.Category} already exists");

        await _categoryRepository.Insert(category);
        await _unitOfWork.Save();
    }
}