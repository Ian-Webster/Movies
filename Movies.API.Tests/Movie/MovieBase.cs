using Moq;
using Movies.API.Controllers;
using Movies.Business.Interfaces;
using NUnit.Framework;

namespace Movies.API.Tests.Movie
{
    public class MovieBase
    {

        protected Mock<IMovieService> MockMovieService;

        [SetUp]
        protected void Setup()
        {
            MockMovieService = new Mock<IMovieService>();
        }

        protected MovieController GetController()
        {
            return new MovieController(MockMovieService.Object);
        }

    }
}
