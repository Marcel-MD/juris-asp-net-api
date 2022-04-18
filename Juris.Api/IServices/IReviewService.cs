using Juris.Domain.Entities;

namespace Juris.Api.IServices;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetAllReviews(long profileId);
    Task<Review> CreateReview(Review review, long profileId);
    Task DeleteReview(long id);
}