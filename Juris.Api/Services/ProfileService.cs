using System.Linq.Expressions;
using System.Net;
using Juris.Api.Exceptions;
using Juris.Api.IServices;
using Juris.Api.Parameters;
using Juris.Dal.Repositories;
using Juris.Domain.Constants;
using Juris.Domain.Entities;
using Juris.Domain.Identity;
using Juris.Resource;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Juris.Api.Services;

public class ProfileService : IProfileService
{
    private readonly IBlobService _blobService;
    private readonly IGenericRepository<ProfileCategory> _categoryRepository;
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IMailService _mailService;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public ProfileService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMailService mailService,
        IBlobService blobService)
    {
        _unitOfWork = unitOfWork;
        _profileRepository = unitOfWork.ProfileRepository;
        _cityRepository = _unitOfWork.CityRepository;
        _categoryRepository = _unitOfWork.ProfileCategoryRepository;
        _userManager = userManager;
        _mailService = mailService;
        _blobService = blobService;
    }

    public async Task<Profile> CreateEmptyProfile(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.UserNotFound, userId));

        var existingProfile = await _profileRepository.Get(p => p.UserId == userId);
        if (existingProfile != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                string.Format(GlobalResource.UserHasProfile, userId));

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
        return profile;
    }

    public async Task DeleteProfile(long profileId, long userId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));
        
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.UserNotFound, userId));
        
        var roles = await _userManager.GetRolesAsync(user);

        if (profile.UserId != userId && !roles.Contains(RoleType.Admin))
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedProfileChange, profileId));

        if (profile.ImageName != null)
            await _blobService.DeleteBlob(profile.ImageName);

        await _profileRepository.Delete(profileId);
        await _unitOfWork.Save();
    }

    public async Task UpdateProfileStatus(string status, long profileId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        var user = await _userManager.FindByIdAsync(profile.UserId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.UserNotFound, profile.UserId));

        profile.Status = status;

        _profileRepository.Update(profile);
        await _unitOfWork.Save();
        try
        {
            await _mailService.SendAsync(
                user.Email,
                GlobalResource.NewProfileStatusSubject,
                string.Format(GlobalResource.NewProfileStatusEmail, status));
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw new HttpResponseException(HttpStatusCode.FailedDependency,
                GlobalResource.CantSendEmail);
        }
    }

    public async Task<IEnumerable<Profile>> GetAllProfiles(ProfileParameters parameters)
    {
        Expression<Func<Profile, bool>> filter = profile =>
            (parameters.Status == null || profile.Status == parameters.Status) &&
            (parameters.CityId == null || profile.CityId == parameters.CityId.Value) &&
            (parameters.CategoryId == null || profile.ProfileCategoryId == parameters.CategoryId.Value);

        Func<IQueryable<Profile>, IOrderedQueryable<Profile>> orderBy = null;
        switch (parameters.SortBy)
        {
            case ProfileSortBy.PriceAsc:
                orderBy = p => p.OrderBy(p => p.Price);
                break;
            case ProfileSortBy.PriceDesc:
                orderBy = p => p.OrderByDescending(p => p.Price);
                break;
            case ProfileSortBy.RatingAsc:
                orderBy = p => p.OrderBy(p => p.Rating);
                break;
            case ProfileSortBy.RatingDesc:
                orderBy = p => p.OrderByDescending(p => p.Rating);
                break;
        }

        var response = await _profileRepository.GetAll(
            filter, orderBy, parameters.PageNumber, parameters.PageSize,
            p => p.City, p => p.ProfileCategory);

        return response;
    }

    public async Task<Profile> GetProfileById(long id)
    {
        var profile = await _profileRepository.Get(p => p.Id == id,
            p => p.City, p => p.ProfileCategory, p => p.Educations, p => p.Experiences, p => p.Reviews);

        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, string.Format(GlobalResource.ProfileNotFound, id));

        return profile;
    }

    public async Task<Profile> CreateProfile(Profile profile, long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.UserNotFound, userId));

        var existingProfile = await _profileRepository.Get(p => p.UserId == userId);
        if (existingProfile != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                string.Format(GlobalResource.UserHasProfile, userId));

        var city = await _cityRepository.GetById(profile.CityId);
        if (city == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.CityNotFound, profile.CityId));

        var category = await _categoryRepository.GetById(profile.ProfileCategoryId);
        if (category == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileCategoryNotFound, profile.ProfileCategoryId));

        profile.UserId = userId;

        await _profileRepository.Insert(profile);
        await _unitOfWork.Save();
        return profile;
    }

    public async Task<Profile> UpdateProfile(Profile profile, long profileId, long userId)
    {
        var existingProfile = await _profileRepository.GetById(profileId);
        if (existingProfile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        if (existingProfile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedProfileChange, profileId));

        var city = await _cityRepository.GetById(profile.CityId);
        if (city == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.CityNotFound, profile.CityId));

        var category = await _categoryRepository.GetById(profile.ProfileCategoryId);
        if (category == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileCategoryNotFound, profile.ProfileCategoryId));

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
        return existingProfile;
    }

    public async Task<string> UpdateProfileImage(IFormFile image, long profileId, long userId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        if (profile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedProfileChange, profileId));

        if (profile.ImageName != null)
            await _blobService.DeleteBlob(profile.ImageName);

        profile.ImageName = await _blobService.UploadBlob(image);

        _profileRepository.Update(profile);
        await _unitOfWork.Save();
        return profile.ImageName;
    }
}