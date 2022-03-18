using AutoMapper;
using Juris.Api.Dtos.Review;
using Juris.Api.Services;
using Juris.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/review")]
public class ReviewController : BaseController
{
    private readonly IReviewService _service;
    private readonly IMapper _mapper;

    public ReviewController(IReviewService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetReviewByProfileId(long profileId)
    {
        var result = await _service.GetAllReviews(profileId);
        var resultDto = _mapper.Map<IEnumerable<ReviewDto>>(result);
        return Ok(resultDto);
    }

    [HttpPost("{profileId}")]
    public async Task<IActionResult> CreateReview(long profileId, CreateReviewDto dto)
    {
        var review = _mapper.Map<Review>(dto);
        await _service.CreateReview(review, profileId);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(long id)
    {
        await _service.DeleteReview(id);
        return Ok();
    }
}