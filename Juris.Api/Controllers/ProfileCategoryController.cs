using AutoMapper;
using Juris.Common.Dtos.ProfileCategory;
using Juris.Api.IServices;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/category")]
public class ProfileCategoryController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IProfileCategoryService _service;

    public ProfileCategoryController(IProfileCategoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _service.GetProfileCategories();
        var resultDto = _mapper.Map<IEnumerable<ProfileCategoryDto>>(result);
        return Ok(resultDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateProfileCategoryDto dto)
    {
        var category = _mapper.Map<ProfileCategory>(dto);
        category = await _service.CreateProfileCategory(category);
        var categoryDto = _mapper.Map<ProfileCategoryDto>(category);
        return Ok(categoryDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(long id)
    {
        await _service.DeleteProfileCategory(id);
        return NoContent();
    }
}