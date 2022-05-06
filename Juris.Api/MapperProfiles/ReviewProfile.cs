using Juris.Common.Dtos.Review;
using Juris.Domain.Entities;
using Profile = AutoMapper.Profile;

namespace Juris.Api.MapperProfiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>().ReverseMap();
        CreateMap<CreateReviewDto, Review>().ReverseMap();
    }
}