using Microsoft.AspNetCore.Http;
using Moq;
using Movies.API.Controllers;
using Movies.Business.Interfaces;
using NUnit.Framework;

namespace Movies.API.Tests.SearchMovies
{
    public class SearchMoviesBase
    {
        protected Mock<HttpContext> MockHttpContext;
        protected Mock<IMovieService> MockMovieService;

        [SetUp]
        protected void Setup()
        {
            MockHttpContext = new Mock<HttpContext>();
            MockMovieService = new Mock<IMovieService>();
        }

        protected SearchMoviesController GetController()
        {
            var controller = new SearchMoviesController(MockMovieService.Object);
            controller.ControllerContext.HttpContext = MockHttpContext.Object;
            return controller;
        }

    }
}
