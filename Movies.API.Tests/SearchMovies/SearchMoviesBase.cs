using Moq;
using Movies.API.Controllers;
using Movies.Business.Interfaces;
using NUnit.Framework;

namespace Movies.API.Tests.SearchMovies
{
    public class SearchMoviesBase
    {

        protected Mock<IMovieService> MockMovieService;

        [SetUp]
        protected void Setup()
        {
            MockMovieService = new Mock<IMovieService>();
        }

        protected SearchMoviesController GetController()
        {
            return new SearchMoviesController(MockMovieService.Object);
        }

    }
}
