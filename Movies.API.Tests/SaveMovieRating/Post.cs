using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
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
        public void ShouldReturn_ExpectedActionResultFor_ValidationFailures(MovieRatingSaveValidationResults validationResult)
        {
            //arrange
            MockRatingService.Setup(s => s.ValidateMovieRatingAsync(It.IsAny<MovieRating>())).Returns(Task.FromResult(validationResult));

            //act
            var asyncResult = GetController().Post(new MovieRating());

            //assert
            var result = asyncResult.Result;
            switch(validationResult)
            {
                case MovieRatingSaveValidationResults.NullRating:
                case MovieRatingSaveValidationResults.InvalidMovieId:
                case MovieRatingSaveValidationResults.InvalidUserId:
                    Assert.IsInstanceOf<BadRequestObjectResult>(result);
                    var badRequest = result as BadRequestObjectResult;
                    Assert.AreEqual(validationResult.ToString(), badRequest.Value);
                    break;
                case MovieRatingSaveValidationResults.MovieNotfound:
                case MovieRatingSaveValidationResults.UserNotFound:
                    Assert.IsInstanceOf<NotFoundObjectResult>(result);
                    var notFound = result as NotFoundObjectResult;
                    Assert.AreEqual(validationResult.ToString(), notFound.Value);
                    break;
            }
        }

        [Test]
        public async Task Should_SaveRatingAsync()
        {
            //arrange
            var rating = new MovieRating { MovieId = 1, Rating = 2, UserId = 3 };

            MockRatingService.Setup(s => s.ValidateMovieRatingAsync(rating)).Returns(Task.FromResult(MovieRatingSaveValidationResults.OK));

            //act
            await GetController().Post(rating);

            //assert
            MockRatingService.Verify(s => s.ValidateMovieRatingAsync(rating), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ShouldReturn_ExpectedActionResultAfter_Saving(bool saved)
        {
            //arrange
            var rating = new MovieRating { MovieId = 1, Rating = 2, UserId = 3 };

            MockRatingService.Setup(s => s.ValidateMovieRatingAsync(rating)).Returns(Task.FromResult(MovieRatingSaveValidationResults.OK));
            MockRatingService.Setup(s => s.SaveRatingAsync(rating)).Returns(Task.FromResult(saved));

            //act
            var asyncResult = GetController().Post(rating);

            //assert
            var result = asyncResult.Result;
            if (saved)
            {
                Assert.IsInstanceOf<OkResult>(result);
            }
            else
            {
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }
        }

    }
}
