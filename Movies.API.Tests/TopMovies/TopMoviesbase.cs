using Moq;
using Movies.API.Controllers;
using Movies.Business.Interfaces;
using NUnit.Framework;

namespace Movies.API.Tests.TopMovies
{
    public class TopMoviesBase
    {

        protected Mock<IMovieService> MockMovieService;
        protected Mock<IUserService> MockUserService;

        [SetUp]
        protected void Setup()
        {
            MockMovieService = new Mock<IMovieService>();
            MockUserService = new Mock<IUserService>();

            MockUserService.Setup(s => s.UserExists(It.IsAny<int>())).Returns(true);

        }

        protected TopMoviesController GetController()
        {
            return new TopMoviesController(MockMovieService.Object, MockUserService.Object);
        }

    }
}
