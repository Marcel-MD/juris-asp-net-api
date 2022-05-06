using System.Net;
using Juris.Common.Exceptions;
using Juris.Api.IServices;
using Juris.Dal.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;

namespace Juris.Api.Services;

public class ReviewService : IReviewService
{
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IGenericRepository<Review> _reviewRepository;
    private readonly IUnitOfWork _unitOfWord;

    public ReviewService(IUnitOfWork unitOfWork)
    {
        _unitOfWord = unitOfWork;
        _profileRepository = unitOfWork.ProfileRepository;
        _reviewRepository = unitOfWork.ReviewRepository;
    }

    public async Task<IEnumerable<Review>> GetAllReviews(long profileId)
    {
        return await _reviewRepository.GetAll(e => e.ProfileId == profileId);
    }

    public async Task<Review> CreateReview(Review review, long profileId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        review.ProfileId = profileId;
        await _reviewRepository.Insert(review);
        await _unitOfWord.Save();
        await UpdateProfileRating(profileId);
        return review;
    }

    public async Task DeleteReview(long id)
    {
        var review = await _reviewRepository.GetById(id);
        if (review == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, string.Format(GlobalResource.ReviewNotFound, id));

        var profileId = review.ProfileId;

        await _reviewRepository.Delete(id);
        await _unitOfWord.Save();
        await UpdateProfileRating(profileId);
    }

    private async Task UpdateProfileRating(long profileId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        var reviews = await _reviewRepository.GetAll(e => e.ProfileId == profileId);

        double rating = 0;

        if (reviews.Count != 0)
            rating = reviews.Average(r => r.Rating);

        profile.Rating = rating;

        _profileRepository.Update(profile);
        await _unitOfWord.Save();
    }
}