using System.Net;
using Juris.Api.Exceptions;
using Juris.Data.Repositories;
using Juris.Models.Constants;
using Juris.Models.Entities;
using Juris.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Juris.Api.Services;

public class ProfileService : IProfileService
{
    private readonly IGenericRepository<Profile> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public ProfileService(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.ProfileRepository;
        _userManager = userManager;
    }

    public async Task<IList<Profile>> GetAllProfiles()
    {
        var response = await _repository.GetAll();

        return response;
    }

    public async Task<Profile> GetProfileById(long id)
    {
        return await _repository.GetById(id);
    }

    public async Task CreateProfile(Profile profile, long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={userId} not found");

        var existingProfile = await _repository.Get(p => p.UserId == userId);
        if (existingProfile != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest, $"User with id={userId} already has a profile");

        profile.UserId = userId;
        
        await _repository.Insert(profile);
        await _unitOfWork.Save();
    }

    public async Task CreateEmptyProfile(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={userId} not found");
        
        var existingProfile = await _repository.Get(p => p.UserId == userId);
        if (existingProfile != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest, $"User with id={userId} already has a profile");
        
        var profile = new Profile()
        {
            UserId = userId,
            FirstName = "",
            LastName = "",
            Description = "",
            PhoneNumber = "",
            Price = 0,
            ProfileType = ProfileType.Lawyer,
            Status = ProfileStatus.Unapproved,
            Address = "",
            City = City.Chisinau
        };
        
        await _repository.Insert(profile);
        await _unitOfWork.Save();
    }

    public async Task UpdateProfile(Profile profile, long profileId, long userId)
    {
        var existingProfile = await _repository.GetById(profileId);
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
        existingProfile.ProfileType = profile.ProfileType;
        existingProfile.Address = profile.Address;
        existingProfile.City = profile.City;
        
        _repository.Update(existingProfile);
        await _unitOfWork.Save();
    }

    public async Task DeleteProfile(long profileId, long userId)
    {
        var profile = await _repository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={profileId} not found");
        
        if (profile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                $"Unauthorized to update profile id={profileId}");

        await _repository.Delete(profileId);
        await _unitOfWork.Save();
    }

    public async Task UpdateProfileStatus(string status, long profileId)
    {
        var profile = await _repository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={profileId} not found");

        profile.Status = status;

        _repository.Update(profile);
        await _unitOfWork.Save();
    }
}