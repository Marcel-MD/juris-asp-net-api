using Juris.Common.Dtos.ProfileCategory;

namespace Juris.Bll.IServices;

public interface IProfileCategoryService
{
    Task<IEnumerable<ProfileCategoryDto>> GetProfileCategories();
    Task<ProfileCategoryDto> CreateProfileCategory(CreateProfileCategoryDto category);
    Task DeleteProfileCategory(long id);
}