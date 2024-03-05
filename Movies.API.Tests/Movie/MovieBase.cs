using Microsoft.AspNetCore.Http;
using Moq;
using Movies.API.Controllers;
using Movies.API.Tests.Shared;
using Movies.Business.Interfaces;
using NUnit.Framework;

namespace Movies.API.Tests.Movie
{
    public class MovieBase: ApiBase
    {

        protected Mock<IMovieService> MockMovieService;
        protected Mock<HttpContext> MockContext;

        [SetUp]
        protected void Setup()
        {
            MockContext = new Mock<HttpContext>();
            MockMovieService = new Mock<IMovieService>();
        }

        protected MovieController GetController()
        {
            var controller = new MovieController(MockMovieService.Object);
            controller.ControllerContext.HttpContext = MockContext.Object;
            return controller;
        }

    }
}
