using Microsoft.AspNetCore.Http;
using Moq;
using Movies.API.Controllers;
using Movies.Business.Interfaces;
using NUnit.Framework;

namespace Movies.API.Tests.SaveMovieRating
{
    public class SaveMovieRatingBase
    {
        protected Mock<HttpContext> MockHttpContext;
        protected Mock<IRatingService> MockRatingService;


        [SetUp]
        protected void SetUp()
        {
            MockHttpContext = new Mock<HttpContext>();
            MockRatingService = new Mock<IRatingService>();
        }

        protected SaveMovieRatingController GetController()
        {
            var controller = new SaveMovieRatingController(MockRatingService.Object);
            controller.ControllerContext.HttpContext = MockHttpContext.Object;
            return controller;
        }

    }
}
