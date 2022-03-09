using Juris.Api.Dtos.Review;
using Juris.Models.Entities;

namespace Juris.Api.MapperProfiles;

public class ReviewProfile : AutoMapper.Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>();
        CreateMap<CreateReviewDto, Review>();
    }
}