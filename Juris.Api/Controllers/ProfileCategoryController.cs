using Juris.Bll.IServices;
using Juris.Common.Dtos.ProfileCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/category")]
public class ProfileCategoryController : BaseController
{
    private readonly IProfileCategoryService _service;

    public ProfileCategoryController(IProfileCategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _service.GetProfileCategories();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateProfileCategoryDto dto)
    {
        var result = await _service.CreateProfileCategory(dto);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(long id)
    {
        await _service.DeleteProfileCategory(id);
        return NoContent();
    }
}