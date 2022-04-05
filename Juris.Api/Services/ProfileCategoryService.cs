using System.Net;
using Juris.Api.Exceptions;
using Juris.Api.IServices;
using Juris.Data.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;

namespace Juris.Api.Services;

public class ProfileCategoryService : IProfileCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<ProfileCategory> _categoryRepository;

    public ProfileCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = _unitOfWork.ProfileCategoryRepository;
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
                string.Format(GlobalResource.CategoryNameExists, category.Category));

        await _categoryRepository.Insert(category);
        await _unitOfWork.Save();
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