﻿using Juris.Common.Dtos.Review;

namespace Juris.Bll.IServices;

public interface IReviewService
{
    /// <summary>
    ///     Gets all reviews by profile id.
    /// </summary>
    /// <param name="profileId">Profile id.</param>
    /// <returns>List of reviews.</returns>
    Task<IEnumerable<ReviewDto>> GetAllReviews(long profileId);

    /// <summary>
    ///     Creates a new review for a profile.
    /// </summary>
    /// <param name="review">Review dto.</param>
    /// <param name="profileId">Id of the profile.</param>
    /// <returns>Created review.</returns>
    Task<ReviewDto> CreateReview(CreateReviewDto review, long profileId);

    /// <summary>
    ///     Deletes review by id.
    /// </summary>
    /// <param name="id">Id of the review.</param>
    Task DeleteReview(long id);
}