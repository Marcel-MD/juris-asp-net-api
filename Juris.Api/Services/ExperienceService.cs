using System.Net;
using Juris.Api.Exceptions;
using Juris.Api.IServices;
using Juris.Dal.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;

namespace Juris.Api.Services;

public class ExperienceService : IExperienceService
{
    private readonly IGenericRepository<Experience> _experienceRepository;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWord;

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

    public async Task<Experience> CreateExperience(Experience experience, long profileId, long userId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        if (profile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedProfileChange, profileId));

        experience.ProfileId = profileId;
        await _experienceRepository.Insert(experience);
        await _unitOfWord.Save();
        return experience;
    }

    public async Task DeleteExperience(long id, long userId)
    {
        var experience = await _experienceRepository.GetById(id);
        if (experience == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ExperienceNotFound, id));

        var profile = await _profileRepository.Get(p => p.UserId == userId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, userId));

        if (experience.ProfileId != profile.Id)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedProfileChange, experience.ProfileId));

        await _experienceRepository.Delete(id);
        await _unitOfWord.Save();
    }
}