using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Juris.Api.Controllers;
using Juris.Common.Dtos.Review;
using Juris.Common.Exceptions;
using Juris.Api.IServices;
using Juris.Api.MapperProfiles;
using Juris.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Juris.Test.Unit;

public class ReviewControllerTests
{
    private readonly Mock<IReviewService> _serviceMock = new();
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new ReviewProfile())));
    private readonly Faker<Review> _testReview = new Faker<Review>()
        .RuleFor(r => r.Id, f => f.UniqueIndex)
        .RuleFor(r => r.ProfileId, f => f.UniqueIndex)
        .RuleFor(r => r.FirstName, f => f.Name.FirstName())
        .RuleFor(r => r.LastName, f => f.Name.LastName())
        .RuleFor(r => r.Email, (f, r) => f.Internet.Email(r.FirstName, r.LastName))
        .RuleFor(r => r.PhoneNumber, f => f.Phone.PhoneNumber())
        .RuleFor(r => r.Description, f => f.Lorem.Lines(1))
        .RuleFor(r => r.Rating, f => f.Random.Int(1, 10))
        .RuleFor(r => r.CreationDate, f => f.Date.Past());

    [Fact]
    public async void GetReviewByProfileId_WithExistentProfile_ReturnsExpectedResult()
    {
        // Arrange
        var reviews = _testReview.GenerateBetween(2, 5);
        _serviceMock
            .Setup(s => s.GetAllReviews(It.IsAny<long>()))
            .ReturnsAsync(reviews);
        var controller = new ReviewController(_serviceMock.Object, _mapper);

        // Act
        var result = await controller.GetReviewsByProfileId(2);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var body = ((ObjectResult) result).Value;
        Assert.IsType<List<ReviewDto>>(body);
        ((ObjectResult) result).Value.Should().BeEquivalentTo(reviews, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async void PostReviewByProfileId_WithNonExistentProfile_ThrowsHttpResponseException()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.CreateReview(It.IsAny<Review>(), It.IsAny<long>()))
            .ThrowsAsync(new HttpResponseException(HttpStatusCode.NotFound, "Profile not found"));
        var controller = new ReviewController(_serviceMock.Object, _mapper);
        var review = _testReview.Generate();
        var dto = _mapper.Map<CreateReviewDto>(review);
        
        // Act
        string type = "";
        try
        {
            var result = await controller.CreateReview(0, dto);
        }
        catch (Exception e)
        {
            type = e.GetType().Name;
        }
        
        // Assert
        Assert.Equal(nameof(HttpResponseException), type);
    }
    
    [Fact]
    public async void PostReviewByProfileId_WithExistentProfile_ReturnsExpectedResult()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.CreateReview(It.IsAny<Review>(), It.IsAny<long>()))
            .ReturnsAsync((Review review, long profileId) => review);
        var controller = new ReviewController(_serviceMock.Object, _mapper);
        var review = _testReview.Generate();
        var dto = _mapper.Map<CreateReviewDto>(review);
        
        // Act
        var result = await controller.CreateReview(0, dto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var body = ((ObjectResult) result).Value;
        Assert.IsType<ReviewDto>(body);
        ((ObjectResult) result).Value.Should().BeEquivalentTo(dto, opt => opt.ExcludingMissingMembers());
    }
    
    [Fact]
    public async void DeleteReviewById_WithNonExistentReview_ThrowsHttpResponseException()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.DeleteReview(It.IsAny<long>()))
            .ThrowsAsync(new HttpResponseException(HttpStatusCode.NotFound, "Review not found"));;
        var controller = new ReviewController(_serviceMock.Object, _mapper);
        
        // Act
        string type = "";
        try
        {
            var result = await controller.DeleteReview(0);
        }
        catch (Exception e)
        {
            type = e.GetType().Name;
        }
        
        // Assert
        Assert.Equal(nameof(HttpResponseException), type);
    }
    
    [Fact]
    public async void DeleteReviewById_WithExistentReview_ReturnsExpectedResult()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.DeleteReview(It.IsAny<long>()));
        var controller = new ReviewController(_serviceMock.Object, _mapper);
        
        // Act
        var result = await controller.DeleteReview(0);
        
        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}