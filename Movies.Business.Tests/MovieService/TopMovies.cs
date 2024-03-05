using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class TopMovies : MovieServiceBase
    {

        [Test]
        public async Task WhenCalling_TopMovies_WithInvalidMovieCount_ExpectException()
        {
            // Arrange / Act / Assert
            await Assert.ThatAsync(() => GetService().TopMovies(0, GetCancellationToken()), Throws.ArgumentException);
        }

        [TestCase((byte)1)]
        [TestCase((byte)2)]
        public async Task WhenCalling_TopMovies_WithValidMovieCount_RepositoryMethodCalled(byte movieCount)
        {
            // Arrange / Act
            await GetService().TopMovies(movieCount, GetCancellationToken());

            // Assert
            MockMovieRepository.Verify(s => s.TopMovies(movieCount, It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
