using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using Juris.Api.Dtos.Review;
using Xunit;

namespace Juris.Test.Integration;

public class ReviewTest : TestBase
{
    private readonly Faker<CreateReviewDto> _testCreateReviewDto = new Faker<CreateReviewDto>()
        .RuleFor(r => r.FirstName, f => f.Name.FirstName())
        .RuleFor(r => r.LastName, f => f.Name.LastName())
        .RuleFor(r => r.Email, (f, r) => f.Internet.Email(r.FirstName, r.LastName))
        .RuleFor(r => r.PhoneNumber, f => f.Phone.PhoneNumber())
        .RuleFor(r => r.Description, f => f.Lorem.Lines(1))
        .RuleFor(r => r.Rating, f => f.Random.Int(1, 10));
    
    [Fact]
    public async void GetReviewsByProfileId_WithExistentProfile_ReturnOk()
    {
        // Arrange
        var profileId = 1;
        
        // Act
        var response = await _client.GetAsync($"api/review/{profileId}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var reviews = await response.Content.ReadFromJsonAsync<List<ReviewDto>>();
        reviews.Should().NotBeEmpty();
    }

    [Fact]
    public async void PostReviewByProfileId_WithValidDto_ReturnOk()
    {
        // Arrange
        var profileId = 1;
        var reviewDto = _testCreateReviewDto.Generate();

        // Act
        var response = await _client.PostAsJsonAsync($"api/review/{profileId}", reviewDto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var review = await response.Content.ReadFromJsonAsync<ReviewDto>();
        reviewDto.Should().BeEquivalentTo(review, opt => opt.ExcludingMissingMembers());
        review.ProfileId.Should().Be(profileId);
    }
    
    [Fact]
    public async void PostReviewByProfileId_WithInvalidDto_ReturnBadRequest()
    {
        // Arrange
        var profileId = 1;
        var reviewDto = _testCreateReviewDto.Generate();
        reviewDto.Description = null;
        
        // Act
        var response = await _client.PostAsJsonAsync($"api/review/{profileId}", reviewDto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async void PostReviewByProfileId_WithNonExistentProfile_ReturnNotFound()
    {
        // Arrange
        var profileId = 777;
        var reviewDto = _testCreateReviewDto.Generate();
        
        // Act
        var response = await _client.PostAsJsonAsync($"api/review/{profileId}", reviewDto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async void DeleteReviewById_WithoutAdminAuthorization_ReturnUnauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = null;
        var reviewId = 100;
        
        // Act
        var response = await _client.DeleteAsync($"api/review/{reviewId}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async void DeleteReviewById_WithNonExistentReview_ReturnNotFound()
    {
        // Arrange
        await Authenticate("marcelvlasenco@gmail.com", "admin123");
        var reviewId = 777;
        
        // Act
        var response = await _client.DeleteAsync($"api/review/{reviewId}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async void DeleteReviewById_WithAdminAuthorization_ReturnNoContent()
    {
        // Arrange
        await Authenticate("marcelvlasenco@gmail.com", "admin123");
        var profileId = 1;
        var reviewDto = _testCreateReviewDto.Generate();
        var response = await _client.PostAsJsonAsync($"api/review/{profileId}", reviewDto);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var review = await response.Content.ReadFromJsonAsync<ReviewDto>();
        var reviewId = review.Id;

        // Act
        response = await _client.DeleteAsync($"api/review/{reviewId}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}