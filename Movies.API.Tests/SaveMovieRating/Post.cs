using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
            MockRatingService.Setup(s => s.ValidateMovieRating(It.IsAny<MovieRating>())).Returns(validationResult);

            //act
            var result = GetController().Post(new MovieRating());

            //assert
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
        public void Should_SaveRating()
        {
            //arrange
            var rating = new MovieRating { MovieId = 1, Rating = 2, UserId = 3 };

            MockRatingService.Setup(s => s.ValidateMovieRating(rating)).Returns(MovieRatingSaveValidationResults.OK);

            //act
            GetController().Post(rating);

            //assert
            MockRatingService.Verify(s => s.ValidateMovieRating(rating), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ShouldReturn_ExpectedActionResultAfter_Saving(bool saved)
        {
            //arrange
            var rating = new MovieRating { MovieId = 1, Rating = 2, UserId = 3 };

            MockRatingService.Setup(s => s.ValidateMovieRating(rating)).Returns(MovieRatingSaveValidationResults.OK);
            MockRatingService.Setup(s => s.SaveRating(rating)).Returns(saved);

            //act
            var result = GetController().Post(rating);

            //assert
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
