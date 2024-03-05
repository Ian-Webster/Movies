using System.Threading;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using dto = Movies.Domain.DTO;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class SaveMovieAsync : MovieServiceBase
    {
        [Test]
        public async Task Should_CallRepositoryMethod()
        {
            // Arrange
            var newMovie = new dto.Movie();

            // Act
            await GetService().SaveMovie(newMovie, GetCancellationToken());

            // Assert
            MockMovieRepository.Verify(s => s.SaveMovie(newMovie, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Should_ReturnTrueWhen_SaveSucceeds(bool saveSuccess)
        {
            // Arrange
            MockMovieRepository.Setup(s => s.SaveMovie(It.IsAny<dto.Movie>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(saveSuccess));

            // Act
            var result = await GetService().SaveMovie(new dto.Movie(), GetCancellationToken());

            // Assert
            if (saveSuccess)
            {
                Assert.That(result, Is.True);
            }
            else
            {
                Assert.That(result, Is.False);
            }
        }

    }
}
