using System;
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
        public void Should_ThrowException_WhenMovieRatingIsNull()
        {
            // Arrange / Act / Assert
            Assert.That(() => GetService().SaveRatingAsync(null), Throws.ArgumentException);
        }

        [TestCase("00000000-0000-0000-0000-000000000000", true, "5FA57BC8-F28D-4486-8E92-50C659F9AEE0", true)]
        [TestCase("A6E255DE-9984-4C97-A804-83956CBD8A24", false, "A5692022-015F-4AA5-8E7D-FA824D48638F", true)]
        [TestCase("7F9325A5-5B90-43FF-A5B0-4CAC7FA0EA65", true, "00000000-0000-0000-0000-000000000000", true)]
        [TestCase("BF10B27C-96DB-42AE-8010-B06C68E3C899", true, "E09EBAFE-F190-4B4F-870E-395C07731D51", false)]
        public void Should_ThrowException_WhenMovingRatingData_IsInvalid(string movieIdString, bool movieExists, string userIdString, bool userExists)
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

            MockMovieService.Setup(s => s.MovieExistsAsync(movieId)).Returns(Task.FromResult(movieExists));
            MockUserService.Setup(s => s.UserExistsAsync(userId)).Returns(Task.FromResult(userExists));

            // Act / Assert
            Assert.That(() => GetService().SaveRatingAsync(movieRating), Throws.ArgumentException);
        }

        [TestCase("305E26B7-8BDD-4E1A-AF89-D9346442FC43", "E8A2D033-E513-43B7-88CE-1E399F005CB0", (byte)1)]
        [TestCase("2AE263FC-5248-487A-88F2-51AE2BB5A8DF", "24383915-8FD1-49AC-8039-5DAAE3CC0AF8", (byte)3)]
        [TestCase("D09C73BC-E0AB-4D91-BDFA-EA09F76C954F", "B56C055A-ECBA-4998-8005-05E0C9A17778", (byte)5)]
        public void Should_CallRatingRepositorySaveRatingAsync_WithExpectedData(string movieIdString, string userIdString, byte rating)
        {
            // Arrange
            var movieId = Guid.Parse(movieIdString);
            var userId = Guid.Parse(userIdString);

            var movieRating = new MovieRating
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating
            };

            MockMovieService.Setup(s => s.MovieExistsAsync(movieId)).Returns(Task.FromResult(true));
            MockUserService.Setup(s => s.UserExistsAsync(userId)).Returns(Task.FromResult(true));

            // Act
            var result = GetService().SaveRatingAsync(movieRating);

            // Assert
            MockRatingRepository.Verify(s => s.SaveRatingAsync(It.Is<MovieRating>(p => p.MovieId == movieId && p.UserId == userId && p.Rating == rating)), Times.Once);

        }

    }
}
