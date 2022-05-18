using Juris.Common.Dtos.ProfileCategory;

namespace Juris.Bll.IServices;

public interface IProfileCategoryService
{
    /// <summary>
    ///     Gets all profile categories.
    /// </summary>
    /// <returns>List of categories.</returns>
    Task<IEnumerable<ProfileCategoryDto>> GetProfileCategories();

    /// <summary>
    ///     Creates a new profile category.
    /// </summary>
    /// <param name="category">Profile category dto.</param>
    /// <returns>Created profile category.</returns>
    Task<ProfileCategoryDto> CreateProfileCategory(CreateProfileCategoryDto category);

    /// <summary>
    ///     Deletes profile category by id. Works only if category is not used in any profile.
    /// </summary>
    /// <param name="id">Profile category id.</param>
    Task DeleteProfileCategory(long id);
}