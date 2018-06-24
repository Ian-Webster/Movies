using Moq;
using Movies.Domain.DTO;
using NUnit.Framework;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class SearchMovies : MovieServiceBase
    {

        [Test]
        public void WhenCalling_SearchMovies_WithNullCriteria_ExpectException()
        {
            //arrange/act/assert
            Assert.That(() => GetService().SearchMovies(null), Throws.ArgumentException);
        }

        [Test]
        public void WhenCalling_SearchMovies_WithInvalidCriteria_ExpectException()
        {
            //arrange/act/assert
            Assert.That(() => GetService().SearchMovies(new MovieSearchCriteria()), Throws.ArgumentException);
        }

        [TestCase("test", 0, false)]
        [TestCase("", 1999, false)]
        [TestCase("", 0, true)]
        [TestCase("test", 1999, true)]
        public void WhenCalling_SearchMovies_WithValidCriteria_RepositoryMethodCalled(string movieTitle, short yearOfRelease, bool hasGenres)
        {
            //arrange
            var criteria = new MovieSearchCriteria
            {
                Title = movieTitle,
                YearOfRelease = yearOfRelease,
                Genres = hasGenres ? GetGenreList() : null
            };

            //act
            var result = GetService().SearchMovies(criteria);

            //assert
            MockMovieRepository.Verify(s => s.SearchMovies(It.Is<MovieSearchCriteria>(p => p.Title == movieTitle 
                                                                                        && p.YearOfRelease == yearOfRelease 
                                                                                        && (hasGenres && p.Genres == p.Genres || p.Genres == null)))
                                                                                        , Times.Once);

        }

    }
}
