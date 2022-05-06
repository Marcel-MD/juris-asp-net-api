using AutoMapper;
using Juris.Common.Dtos.Review;
using Juris.Api.IServices;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/review")]
public class ReviewController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IReviewService _service;

    public ReviewController(IReviewService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetReviewsByProfileId(long profileId)
    {
        var result = await _service.GetAllReviews(profileId);
        var resultDto = _mapper.Map<IEnumerable<ReviewDto>>(result);
        return Ok(resultDto);
    }

    [HttpPost("{profileId}")]
    public async Task<IActionResult> CreateReview(long profileId, CreateReviewDto dto)
    {
        var review = _mapper.Map<Review>(dto);
        review = await _service.CreateReview(review, profileId);
        var reviewDto = _mapper.Map<ReviewDto>(review);
        return Ok(reviewDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(long id)
    {
        await _service.DeleteReview(id);
        return NoContent();
    }
}