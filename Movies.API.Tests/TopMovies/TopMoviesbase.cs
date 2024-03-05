using System;
using System.Threading;
using Moq;
using Movies.API.Controllers;
using Movies.Business.Interfaces;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Movies.API.Tests.TopMovies
{
    public class TopMoviesBase
    {
        protected Mock<HttpContext> MockHttpContext;
        protected Mock<IMovieService> MockMovieService;
        protected Mock<IUserService> MockUserService;

        [SetUp]
        protected void Setup()
        {
            MockHttpContext = new Mock<HttpContext>();
            MockMovieService = new Mock<IMovieService>();
            MockUserService = new Mock<IUserService>();

            MockUserService.Setup(s => s.UserExists(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

        }

        protected TopMoviesController GetController()
        {
            var controller = new TopMoviesController(MockMovieService.Object, MockUserService.Object);
            controller.ControllerContext.HttpContext = MockHttpContext.Object;
            return controller;
        }

    }
}
