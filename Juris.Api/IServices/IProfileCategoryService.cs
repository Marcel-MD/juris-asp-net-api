﻿using Juris.Domain.Entities;

namespace Juris.Api.IServices;

public interface IProfileCategoryService
{
    Task<IEnumerable<ProfileCategory>> GetProfileCategories();
    Task CreateProfileCategory(ProfileCategory category);
    Task DeleteProfileCategory(long id);
}