using System.Net;
using Juris.Api.Exceptions;
using Juris.Data.Repositories;
using Juris.Domain.Entities;

namespace Juris.Api.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWord;
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IGenericRepository<Review> _reviewRepository;

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

    public async Task CreateReview(Review review, long profileId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={profileId} not found");
        
        review.ProfileId = profileId;
        await _reviewRepository.Insert(review);
        await _unitOfWord.Save();
        await UpdateProfileRating(profileId);
    }

    public async Task DeleteReview(long id)
    {
        var review = await _reviewRepository.GetById(id);
        if (review == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Review with id={id} not found");

        var profileId = review.ProfileId;
        
        await _reviewRepository.Delete(id);
        await _unitOfWord.Save();
        await UpdateProfileRating(profileId);
    }

    private async Task UpdateProfileRating(long profileId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound, $"Profile with id={profileId} not found");

        var reviews = await _reviewRepository.GetAll(e => e.ProfileId == profileId);

        double rating = 0;
        
        if (reviews.Count != 0)
            rating = reviews.Average(r => r.Rating);

        profile.Rating = rating;
        
        _profileRepository.Update(profile);
        await _unitOfWord.Save();
    }
}