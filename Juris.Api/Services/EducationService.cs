using System.Net;
using AutoMapper;
using Juris.Common.Exceptions;
using Juris.Api.IServices;
using Juris.Common.Dtos.Education;
using Juris.Dal.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;
using Profile = Juris.Domain.Entities.Profile;

namespace Juris.Api.Services;

public class EducationService : IEducationService
{
    private readonly IGenericRepository<Education> _educationRepository;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWord;
    private readonly IMapper _mapper;

    public EducationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWord = unitOfWork;
        _mapper = mapper;
        _profileRepository = unitOfWork.ProfileRepository;
        _educationRepository = unitOfWork.EducationRepository;
    }

    public async Task<IEnumerable<EducationDto>> GetAllEducation(long profileId)
    {
        var educations = await _educationRepository.GetAll(e => e.ProfileId == profileId);
        return _mapper.Map<IEnumerable<EducationDto>>(educations);
    }

    public async Task<EducationDto> CreateEducation(CreateEducationDto dto, long profileId, long userId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        if (profile.UserId != userId)
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                string.Format(GlobalResource.UnauthorizedProfileChange, profileId));
        
        var education = _mapper.Map<Education>(dto);
        education.ProfileId = profileId;
        await _educationRepository.Insert(education);
        await _unitOfWord.Save();
        return _mapper.Map<EducationDto>(education);
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