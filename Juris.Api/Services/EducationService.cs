using System.Net;
using Juris.Api.Exceptions;
using Juris.Api.IServices;
using Juris.Data.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;

namespace Juris.Api.Services;

public class EducationService : IEducationService
{
    private readonly IGenericRepository<Education> _educationRepository;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWord;

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

    public async Task<Education> CreateEducation(Education education, long profileId, long userId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        if (profile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedProfileChange, profileId));

        education.ProfileId = profileId;
        await _educationRepository.Insert(education);
        await _unitOfWord.Save();
        return education;
    }

    public async Task DeleteEducation(long id, long userId)
    {
        var education = await _educationRepository.GetById(id);
        if (education == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.EducationNotFound, id));

        var profile = await _profileRepository.Get(p => p.UserId == userId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, userId));

        if (education.ProfileId != profile.Id)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedProfileChange, education.ProfileId));

        await _educationRepository.Delete(id);
        await _unitOfWord.Save();
    }
}