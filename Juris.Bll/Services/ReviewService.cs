using System.Net;
using AutoMapper;
using Juris.Common.Exceptions;
using Juris.Bll.IServices;
using Juris.Common.Dtos.Review;
using Juris.Dal.Repositories;
using Juris.Domain.Entities;
using Juris.Resource;
using Profile = Juris.Domain.Entities.Profile;

namespace Juris.Bll.Services;

public class ReviewService : IReviewService
{
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IGenericRepository<Review> _reviewRepository;
    private readonly IUnitOfWork _unitOfWord;
    private readonly IMapper _mapper;

    public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWord = unitOfWork;
        _mapper = mapper;
        _profileRepository = unitOfWork.ProfileRepository;
        _reviewRepository = unitOfWork.ReviewRepository;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllReviews(long profileId)
    {
        var reviews = await _reviewRepository.GetAll(e => e.ProfileId == profileId);
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto> CreateReview(CreateReviewDto dto, long profileId)
    {
        var profile = await _profileRepository.GetById(profileId);
        if (profile == null)
            throw new HttpResponseException(HttpStatusCode.NotFound,
                string.Format(GlobalResource.ProfileNotFound, profileId));

        var review = _mapper.Map<Review>(dto);
        review.ProfileId = profileId;
        await _reviewRepository.Insert(review);
        await _unitOfWord.Save();
        await UpdateProfileRating(profileId);
        return _mapper.Map<ReviewDto>(review);;
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