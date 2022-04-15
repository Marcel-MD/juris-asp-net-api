using Juris.Domain.Entities;

namespace Juris.Api.IServices;

public interface IProfileCategoryService
{
    Task<IEnumerable<ProfileCategory>> GetProfileCategories();
    Task<ProfileCategory> CreateProfileCategory(ProfileCategory category);
    Task DeleteProfileCategory(long id);
}