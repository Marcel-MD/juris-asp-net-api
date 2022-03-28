using Juris.Domain.Entities;

namespace Juris.Api.Services;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetAllReviews(long profileId);
    Task CreateReview(Review review, long profileId);
    Task DeleteReview(long id);
}