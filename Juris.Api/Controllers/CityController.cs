using AutoMapper;
using Juris.Common.Dtos.City;
using Juris.Api.IServices;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/city")]
public class CityController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ICityService _service;

    public CityController(ICityService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var result = await _service.GetCities();
        var resultDto = _mapper.Map<IEnumerable<CityDto>>(result);
        return Ok(resultDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateCity(CreateCityDto dto)
    {
        var city = _mapper.Map<City>(dto);
        city = await _service.CreateCity(city);
        var cityDto = _mapper.Map<CityDto>(city);
        return Ok(cityDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(long id)
    {
        await _service.DeleteCity(id);
        return NoContent();
    }
}