using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Movies.API.Tests.SaveMovieRating
{
    [TestFixture]
    public class Post : SaveMovieRatingBase
    {

        [TestCase(MovieRatingSaveValidationResults.InvalidMovieId)]
        [TestCase(MovieRatingSaveValidationResults.InvalidUserId)]
        [TestCase(MovieRatingSaveValidationResults.MovieNotfound)]
        [TestCase(MovieRatingSaveValidationResults.NullRating)]
        [TestCase(MovieRatingSaveValidationResults.UserNotFound)]
        public async Task ShouldReturn_ExpectedActionResultFor_ValidationFailures(MovieRatingSaveValidationResults validationResult)
        {
            // Arrange
            MockRatingService.Setup(s => s.ValidateMovieRating(It.IsAny<MovieRating>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(validationResult));

            // Act
            var result = await GetController().Post(new MovieRating());

            // Assert
            switch(validationResult)
            {
                case MovieRatingSaveValidationResults.NullRating:
                case MovieRatingSaveValidationResults.InvalidMovieId:
                case MovieRatingSaveValidationResults.InvalidUserId:
                    Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
                    var badRequest = result as BadRequestObjectResult;
                    Assert.That(validationResult.ToString(), Is.EqualTo(badRequest.Value));
                    break;
                case MovieRatingSaveValidationResults.MovieNotfound:
                case MovieRatingSaveValidationResults.UserNotFound:
                    Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
                    var notFound = result as NotFoundObjectResult;
                    Assert.That(validationResult.ToString(), Is.EqualTo(notFound.Value));
                    break;
            }
        }

        [Test]
        public async Task Should_SaveRatingAsync()
        {
            // Arrange
            var rating = new MovieRating { MovieId = Guid.NewGuid(), Rating = 2, UserId = Guid.NewGuid() };

            MockRatingService.Setup(s => s.ValidateMovieRating(rating, It.IsAny<CancellationToken>())).Returns(Task.FromResult(MovieRatingSaveValidationResults.OK));

            // Act
            await GetController().Post(rating);

            // Assert
            MockRatingService.Verify(s => s.ValidateMovieRating(rating, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task ShouldReturn_ExpectedActionResultAfter_Saving(bool saved)
        {
            // Arrange
            var rating = new MovieRating { MovieId = Guid.NewGuid(), Rating = 2, UserId = Guid.NewGuid() };

            MockRatingService.Setup(s => s.ValidateMovieRating(rating, It.IsAny<CancellationToken>())).Returns(Task.FromResult(MovieRatingSaveValidationResults.OK));
            MockRatingService.Setup(s => s.SaveRating(rating, It.IsAny<CancellationToken>())).Returns(Task.FromResult(saved));

            // Act
            var result = await GetController().Post(rating);

            // Assert
            if (saved)
            {
                Assert.That(result, Is.InstanceOf<OkResult>());
            }
            else
            {
                Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            }
        }

    }
}
