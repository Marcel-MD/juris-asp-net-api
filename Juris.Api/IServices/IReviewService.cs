using Juris.Common.Dtos.Review;

namespace Juris.Api.IServices;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllReviews(long profileId);
    Task<ReviewDto> CreateReview(CreateReviewDto review, long profileId);
    Task DeleteReview(long id);
}