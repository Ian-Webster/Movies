using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

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
            Assert.That(() => GetService().MovieExistsAsync(movieId), Throws.ArgumentException);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void WhenCalling_MovieExists_WithValidMovieId_RepositoryMethodCalled(int movieId)
        {
            //arrange
            MockUserService.Setup(s => s.UserExistsAsync(movieId)).Returns(Task.FromResult(true));

            //act
            var result = GetService().MovieExistsAsync(movieId);

            //assert
            MockMovieRepository.Verify(s => s.MovieExistsAsync(movieId), Times.Once);
        }
    }
}
