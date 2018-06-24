using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using NUnit.Framework;

namespace Movies.Business.Tests.RatingService
{
    [TestFixture]
    public class ValidateMovieRating : RatingServiceBase
    {

        [Test]
        public void WhenCalling_ValidateMovieRating_WithNullMovieRating_ExpectedResultReturned()
        {
            //arrange
            var expectedResult = MovieRatingSaveValidationResults.NullRating;

            //act
            var result = GetService().ValidateMovieRating(null);

            //assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(-1, true, 1, true, MovieRatingSaveValidationResults.InvalidMovieId)]
        [TestCase(0, true, 1, true, MovieRatingSaveValidationResults.InvalidMovieId)]
        [TestCase(1, false, 1, true, MovieRatingSaveValidationResults.MovieNotfound)]
        [TestCase(1, true, -1, true, MovieRatingSaveValidationResults.InvalidUserId)]
        [TestCase(1, true, 0, true, MovieRatingSaveValidationResults.InvalidUserId)]
        [TestCase(1, true, 1, false, MovieRatingSaveValidationResults.UserNotFound)]
        [TestCase(1, true, 1, true, MovieRatingSaveValidationResults.OK)]
        public void WhenCalling_ValidateMovieRating_WithInvalidMovieRating_ExpectResultReturned(int movieId, bool movieExists, int userId, bool userExists, MovieRatingSaveValidationResults expectedResult)
        {
            //arrange
            var movieRating = new MovieRating
            {
                MovieId = movieId,
                UserId = userId,
                Rating = 1
            };

            MockUserService.Setup(s => s.UserExists(userId)).Returns(userExists);
            MockMovieService.Setup(s => s.MovieExists(movieId)).Returns(movieExists);

            //act
            var result = GetService().ValidateMovieRating(movieRating);

            //assert
            Assert.AreEqual(expectedResult, result);
        }

    }
}
