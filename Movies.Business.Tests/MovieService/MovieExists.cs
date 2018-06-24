using Moq;
using NUnit.Framework;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class MovieExists: MovieServiceBase
    {

        [TestCase(-1)]
        [TestCase(0)]
        public void WhenCalling_MovieExists_WithInvalidMovieId_ExpectException(int movieId)
        {
            //arrange/act/assert
            Assert.That(() => GetService().MovieExists(movieId), Throws.ArgumentException);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void WhenCalling_MovieExists_WithValidMovieId_RepositoryMethodCalled(int movieId)
        {
            //arrange
            MockUserService.Setup(s => s.UserExists(movieId)).Returns(true);

            //act
            var result = GetService().MovieExists(movieId);

            //assert
            MockMovieRepository.Verify(s => s.MovieExists(movieId), Times.Once);
        }
    }
}
