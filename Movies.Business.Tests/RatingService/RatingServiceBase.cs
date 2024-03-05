using Moq;
using Movies.Business.Interfaces;
using Movies.Business.Tests.Shared;
using Movies.Repository.Interfaces;

namespace Movies.Business.Tests.RatingService
{
    public class RatingServiceBase: BusinessServiceBase
    {
        protected readonly Mock<IRatingRepository> MockRatingRepository;
        protected readonly Mock<IMovieService> MockMovieService;
        protected readonly Mock<IUserService> MockUserService;

        public RatingServiceBase()
        {
            MockRatingRepository = new Mock<IRatingRepository>();
            MockMovieService = new Mock<IMovieService>();
            MockUserService = new Mock<IUserService>();
        }

        public IRatingService GetService()
        {
            return new Business.RatingService(MockRatingRepository.Object, MockMovieService.Object, MockUserService.Object);
        }

    }
}
