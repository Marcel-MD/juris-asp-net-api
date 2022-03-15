using System.Net;
using Juris.Api.Exceptions;
using Juris.Data.Repositories;
using Juris.Models.Entities;

namespace Juris.Api.Services;

public class EducationService : IEducationService
{
    private readonly IUnitOfWork _unitOfWord;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IGenericRepository<Education> _educationRepository;

    public EducationService(IUnitOfWork unitOfWork)
    {
        _unitOfWord = unitOfWork;
        _profileRepository = unitOfWork.ProfileRepository;
        _educationRepository = unitOfWork.EducationRepository;
    }
    
    public async Task<IEnumerable<Education>> GetAllEducation(long profileId)
    {
        return await _educationRepository.GetAll(e => e.ProfileId == profileId);
    }

    public async Task CreateEducation(Education education, long profileId, long userId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={profileId} not found");
        
        if (profile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized, $"Unauthorized to change profile id={profileId}");

        education.ProfileId = profileId;
        await _educationRepository.Insert(education);
        await _unitOfWord.Save();
    }

    public async Task DeleteEducation(long id, long userId)
    {
        var education = await _educationRepository.GetById(id);
        if (education == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Education with id={id} not found");
        
        var profile = await _profileRepository.Get(p => p.UserId == userId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile for user id={userId} not found");

        if (education.ProfileId != profile.Id)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                $"Unauthorized to change profile id={education.ProfileId}");

        await _educationRepository.Delete(id);
        await _unitOfWord.Save();
    }
}