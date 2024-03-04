using System;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class MovieExists: MovieServiceBase
    {

        [Test]
        public void Should_ThrowException_When_MovieIdIsEmpty()
        {
            // Arrange / Act / Assert
            Assert.That(() => GetService().MovieExistsAsync(Guid.Empty), Throws.ArgumentException);
        }

        [TestCase("C79F65F8-C899-4B46-8F39-F734A20AD70A")]
        [TestCase("3A931C89-9158-45BA-8CC3-FF683314FCD4")]
        [TestCase("D3A3A3A3-3A3A-3A3A-3A3A-3A3A3A3A3A3A")]
        public void Should_CallMovieServiceMovieExistsAsync_When_MovieIsIsNotEmpty(string movieIdString)
        {
            // Arrange
            var movieId = Guid.Parse(movieIdString);
            MockUserService.Setup(s => s.UserExistsAsync(movieId)).Returns(Task.FromResult(true));

            // Act
            var result = GetService().MovieExistsAsync(movieId);

            // Assert
            MockMovieRepository.Verify(s => s.MovieExistsAsync(movieId), Times.Once);
        }
    }
}
