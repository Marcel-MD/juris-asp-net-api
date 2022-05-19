using Juris.Bll.IServices;
using Juris.Common.Dtos.City;
using Juris.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/city")]
public class CityController : BaseController
{
    private readonly ICityService _service;

    public CityController(ICityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var result = await _service.GetCities();
        return Ok(result);
    }

    [Authorize(Roles = RoleType.Admin)]
    [HttpPost]
    public async Task<IActionResult> CreateCity(CreateCityDto dto)
    {
        var result = await _service.CreateCity(dto);
        return Ok(result);
    }

    [Authorize(Roles = RoleType.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(long id)
    {
        await _service.DeleteCity(id);
        return NoContent();
    }
}