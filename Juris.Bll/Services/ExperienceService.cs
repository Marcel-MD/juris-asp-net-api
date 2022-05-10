using System.Net;
using AutoMapper;
using Juris.Common.Exceptions;
using Juris.Bll.IServices;
using Juris.Common.Dtos.Experience;
using Juris.Dal.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;
using Profile = Juris.Domain.Entities.Profile;

namespace Juris.Bll.Services;

public class ExperienceService : IExperienceService
{
    private readonly IGenericRepository<Experience> _experienceRepository;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWord;
    private readonly IMapper _mapper;

    public ExperienceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWord = unitOfWork;
        _mapper = mapper;
        _profileRepository = unitOfWork.ProfileRepository;
        _experienceRepository = unitOfWork.ExperienceRepository;
    }

    public async Task<IEnumerable<ExperienceDto>> GetAllExperience(long profileId)
    {
        var experiences = await _experienceRepository.GetAll(e => e.ProfileId == profileId);
        return _mapper.Map<IEnumerable<ExperienceDto>>(experiences);
    }

    public async Task<ExperienceDto> CreateExperience(CreateExperienceDto dto, long profileId, long userId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        if (profile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedProfileChange, profileId));

        var experience = _mapper.Map<Experience>(dto);
        experience.ProfileId = profileId;
        await _experienceRepository.Insert(experience);
        await _unitOfWord.Save();
        return _mapper.Map<ExperienceDto>(experience);
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