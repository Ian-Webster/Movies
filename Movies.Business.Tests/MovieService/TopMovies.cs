using Moq;
using NUnit.Framework;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class TopMovies : MovieServiceBase
    {

        [Test]
        public void WhenCalling_TopMovies_WithInvalidMovieCount_ExpectException()
        {
            //arrange/act/assert
            Assert.That(() => GetService().TopMovies(0), Throws.ArgumentException);
        }

        [TestCase((byte)1)]
        [TestCase((byte)2)]
        public void WhenCalling_TopMovies_WithValidMovieCount_RepositoryMethodCalled(byte movieCount)
        {
            //arrange/act
            var result = GetService().TopMovies(movieCount);

            //assert
            MockMovieRepository.Verify(s => s.TopMovies(movieCount), Times.Once);
        }

    }
}
