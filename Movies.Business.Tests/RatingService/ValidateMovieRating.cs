using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Movies.Business.Tests.RatingService
{
    [TestFixture]
    public class ValidateMovieRating : RatingServiceBase
    {

        [Test]
        public void Should_ReturnExpectedResult_WhenMovieRatingIsNull()
        {
            // Arrange
            var expectedResult = MovieRatingSaveValidationResults.NullRating;

            // Act 
            var result = GetService().ValidateMovieRatingAsync(null);

            // Assert
            Assert.That(expectedResult, Is.EqualTo(result.Result));
        }

        [TestCase("00000000-0000-0000-0000-000000000000", true, "987F3515-84FD-4ADD-8F69-A2153B74B5D6", true, MovieRatingSaveValidationResults.InvalidMovieId)]
        [TestCase("174B8B01-E6BE-4497-8461-957CCCFE035E", false, "BA0D5B8A-5A0E-4E4B-86C3-9E3DCEC4F819", true, MovieRatingSaveValidationResults.MovieNotfound)]
        [TestCase("2D569C54-9F36-4180-8E66-433D03046263", true, "00000000-0000-0000-0000-000000000000", true, MovieRatingSaveValidationResults.InvalidUserId)]
        [TestCase("9A01E29A-D22A-4462-90E7-EA5C2A6D7A57", true, "39948633-C91A-4B0C-8C41-9CDEF8CE0A8F", false, MovieRatingSaveValidationResults.UserNotFound)]
        [TestCase("069606E5-2E49-40AE-84D9-B3FF635B0E95", true, "AFF4E1BA-A113-4E22-8B0B-DADCFC1A191C", true, MovieRatingSaveValidationResults.OK)]
        public void Should_ReturnExpectedResult_WhenMovieRatingDataIsInvalid (string movieIdString, bool movieExists, string userIdString, bool userExists, MovieRatingSaveValidationResults expectedResult)
        {
            // Arrange
            var movieId = Guid.Parse(movieIdString);
            var userId = Guid.Parse(userIdString);

            var movieRating = new MovieRating
            {
                MovieId = movieId,
                UserId = userId,
                Rating = 1
            };

            MockUserService.Setup(s => s.UserExistsAsync(userId)).Returns(Task.FromResult(userExists));
            MockMovieService.Setup(s => s.MovieExistsAsync(movieId)).Returns(Task.FromResult(movieExists));

            // Act 
            var result = GetService().ValidateMovieRatingAsync(movieRating);

            // Assert
            Assert.That(expectedResult, Is.EqualTo(result.Result));
        }

    }
}
