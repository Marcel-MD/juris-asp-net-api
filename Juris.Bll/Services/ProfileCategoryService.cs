using System.Net;
using AutoMapper;
using Juris.Common.Exceptions;
using Juris.Bll.IServices;
using Juris.Common.Dtos.ProfileCategory;
using Juris.Dal.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;

namespace Juris.Bll.Services;

public class ProfileCategoryService : IProfileCategoryService
{
    private readonly IGenericRepository<ProfileCategory> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProfileCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryRepository = _unitOfWork.ProfileCategoryRepository;
    }

    public async Task<IEnumerable<ProfileCategoryDto>> GetProfileCategories()
    {
        var categories = await _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<ProfileCategoryDto>>(categories);
    }

    public async Task<ProfileCategoryDto> CreateProfileCategory(CreateProfileCategoryDto categoryDto)
    {
        var cat = await _categoryRepository.Get(c => c.Category == categoryDto.Category);
        if (cat != null)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                string.Format(GlobalResource.CategoryNameExists, categoryDto.Category));

        var category = _mapper.Map<ProfileCategory>(categoryDto);
        
        await _categoryRepository.Insert(category);
        await _unitOfWork.Save();

        return _mapper.Map<ProfileCategoryDto>(category);
    }

    public async Task DeleteProfileCategory(long id)
    {
        var cat = await _categoryRepository.GetById(id);
        if (cat == null)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                string.Format(GlobalResource.ProfileCategoryNotFound, id));

        try
        {
            await _categoryRepository.Delete(id);
            await _unitOfWork.Save();
        }
        catch (Exception e)
        {
            throw new HttpResponseException(HttpStatusCode.BadRequest, GlobalResource.CantDeleteResource);
        }
    }
}