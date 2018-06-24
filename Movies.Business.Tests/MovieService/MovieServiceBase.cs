using Moq;
using Movies.Business.Interfaces;
using Movies.Domain.Enums;
using Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Business.Tests.MovieService
{
    public class MovieServiceBase
    {
        protected readonly Mock<IMovieRepository> MockMovieRepository;
        protected readonly Mock<IUserService> MockUserService;

        public MovieServiceBase()
        {
            MockMovieRepository = new Mock<IMovieRepository>();
            MockUserService = new Mock<IUserService>();
        }

        protected IMovieService GetService()
        {
            return new Business.MovieService(MockMovieRepository.Object, MockUserService.Object);
        }

        protected List<Genres> GetGenreList()
        {
            return Enum.GetValues(typeof(Genres)).Cast<Genres>().ToList();
        }

    }
}
