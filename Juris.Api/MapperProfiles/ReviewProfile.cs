using Juris.Api.Dtos.Review;
using Juris.Domain.Entities;

namespace Juris.Api.MapperProfiles;

public class ReviewProfile : AutoMapper.Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>();
        CreateMap<CreateReviewDto, Review>();
    }
}