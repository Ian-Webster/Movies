using Moq;
using Movies.API.Controllers;
using Movies.Business.Interfaces;
using NUnit.Framework;

namespace Movies.API.Tests.SaveMovieRating
{
    public class SaveMovieRatingBase
    {

        protected Mock<IRatingService> MockRatingService;

        public SaveMovieRatingBase()
        {
            MockRatingService = new Mock<IRatingService>();
        }

        [SetUp]
        protected void SetUp()
        {
            
        }

        protected SaveMovieRatingController GetController()
        {
            return new SaveMovieRatingController(MockRatingService.Object);
        }

    }
}
