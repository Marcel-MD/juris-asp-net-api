using System.Net;
using Juris.Api.Exceptions;
using Juris.Data.Repositories;
using Juris.Domain.Entities;

namespace Juris.Api.Services;

public class ExperienceService : IExperienceService
{
    private readonly IUnitOfWork _unitOfWord;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IGenericRepository<Experience> _experienceRepository;

    public ExperienceService(IUnitOfWork unitOfWork)
    {
        _unitOfWord = unitOfWork;
        _profileRepository = unitOfWork.ProfileRepository;
        _experienceRepository = unitOfWork.ExperienceRepository;
    }
    
    public async Task<IEnumerable<Experience>> GetAllExperience(long profileId)
    {
        return await _experienceRepository.GetAll(e => e.ProfileId == profileId);
    }

    public async Task CreateExperience(Experience experience, long profileId, long userId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={profileId} not found");
        
        if (profile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized, $"Unauthorized to change profile id={profileId}");

        experience.ProfileId = profileId;
        await _experienceRepository.Insert(experience);
        await _unitOfWord.Save();
    }

    public async Task DeleteExperience(long id, long userId)
    {
        var experience = await _experienceRepository.GetById(id);
        if (experience == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Experience with id={id} not found");
        
        var profile = await _profileRepository.Get(p => p.UserId == userId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile for user id={userId} not found");

        if (experience.ProfileId != profile.Id)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                $"Unauthorized to change profile id={experience.ProfileId}");

        await _experienceRepository.Delete(id);
        await _unitOfWord.Save();
    }
}