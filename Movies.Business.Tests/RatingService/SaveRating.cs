using Moq;
using Movies.Domain.DTO;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Movies.Business.Tests.RatingService
{
    [TestFixture]
    public class SaveRating : RatingServiceBase
    {
        [Test]
        public void WhenCalling_SaveRating_WithNullMovieRating_ExpectException()
        {
            //arrange/act/assert
            Assert.That(() => GetService().SaveRatingAsync(null), Throws.ArgumentException);
        }

        [TestCase(-1, true, 1, true)]
        [TestCase(0, true, 1, true)]
        [TestCase(1, false, 1, true)]
        [TestCase(1, true, -1, true)]
        [TestCase(1, true, 0, true)]
        [TestCase(1, true, 1, false)]
        public void WhenCalling_SaveRating_WithInvalidMovieRating_ExceptException(int movieId, bool movieExists, int userId, bool userExists)
        {
            //arrange
            var movieRating = new MovieRating
            {
                MovieId = movieId,
                UserId = userId,
                Rating = 1
            };

            MockMovieService.Setup(s => s.MovieExistsAsync(movieId)).Returns(Task.FromResult(movieExists));
            MockUserService.Setup(s => s.UserExistsAsync(userId)).Returns(Task.FromResult(userExists));

            //act/assert
            Assert.That(() => GetService().SaveRatingAsync(movieRating), Throws.ArgumentException);
        }

        [TestCase(1, 3, (byte)1)]
        [TestCase(3, 4, (byte)3)]
        public void WhenCalling_SaveRating_WithValidMovieRating_RepositoryMethodCalled(int movieId, int userId, byte rating)
        {
            //arrange
            var movieRating = new MovieRating
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating
            };

            MockMovieService.Setup(s => s.MovieExistsAsync(movieId)).Returns(Task.FromResult(true));
            MockUserService.Setup(s => s.UserExistsAsync(userId)).Returns(Task.FromResult(true));

            //act
            var result = GetService().SaveRatingAsync(movieRating);

            //assert
            MockRatingRepository.Verify(s => s.SaveRatingAsync(It.Is<MovieRating>(p => p.MovieId == movieId && p.UserId == userId && p.Rating == rating)), Times.Once);

        }

    }
}
