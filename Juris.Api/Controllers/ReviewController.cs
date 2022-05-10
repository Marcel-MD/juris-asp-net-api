using Juris.Common.Dtos.Review;
using Juris.Bll.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[Route("api/review")]
public class ReviewController : BaseController
{
    private readonly IReviewService _service;

    public ReviewController(IReviewService service)
    {
        _service = service;
    }

    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetReviewsByProfileId(long profileId)
    {
        var result = await _service.GetAllReviews(profileId);
        return Ok(result);
    }

    [HttpPost("{profileId}")]
    public async Task<IActionResult> CreateReview(long profileId, CreateReviewDto dto)
    {
        var result = await _service.CreateReview(dto, profileId);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(long id)
    {
        await _service.DeleteReview(id);
        return NoContent();
    }
}