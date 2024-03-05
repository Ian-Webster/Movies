using System.Threading;
using System.Threading.Tasks;
using Moq;
using Movies.Domain.DTO;
using NUnit.Framework;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class SearchMovies : MovieServiceBase
    {

        [Test]
        public async Task WhenCalling_SearchMovies_WithNullCriteria_ExpectException()
        {
            // Arrange / Act / Assert
            await Assert.ThatAsync(() => GetService().SearchMovies(null, GetCancellationToken()), Throws.ArgumentException);
        }

        [Test]
        public async Task WhenCalling_SearchMovies_WithInvalidCriteria_ExpectException()
        {
            // Arrange / Act / Assert
            await Assert.ThatAsync(() => GetService().SearchMovies(new MovieSearchCriteria(), GetCancellationToken()), Throws.ArgumentException);
        }

        [TestCase("test", 0, false)]
        [TestCase("", 1999, false)]
        [TestCase("", 0, true)]
        [TestCase("test", 1999, true)]
        public async Task WhenCalling_SearchMovies_WithValidCriteria_RepositoryMethodCalled(string movieTitle, short yearOfRelease, bool hasGenres)
        {
            // Arrange
            var criteria = new MovieSearchCriteria
            {
                Title = movieTitle,
                YearOfRelease = yearOfRelease,
                Genres = hasGenres ? GetGenreList() : null
            };

            // Act
            await GetService().SearchMovies(criteria, GetCancellationToken());

            // Assert
            MockMovieRepository.Verify(s => s.SearchMovies(It.Is<MovieSearchCriteria>(p => p.Title == movieTitle 
                                                                                        && p.YearOfRelease == yearOfRelease 
                                                                                        && (hasGenres && p.Genres == p.Genres || p.Genres == null)), 
                                                                                        It.IsAny<CancellationToken>())
                                                                                        , Times.Once);

        }

    }
}
