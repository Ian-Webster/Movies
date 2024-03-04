using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Domain.Enums;
using Movies.Repository.Tests.DataGeneration;
using Newtonsoft.Json;
using dto = Movies.Domain.DTO;
using repo = Movies.Repository.Entities;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Movies.Repository.Tests.Movie;

[TestFixture]
public class GetMovieAsync : MovieRepositoryBase
{
    [Test]
    public async Task Should_ReturnNull_WhenMovieNotFound()
    {
        // Arrange
        var movies = MovieDataGeneration.GetRandomMovies(5);
        InsertMovies(movies);

        // Act
        var result = await GetRepository().GetMovieAsync(100);

        // Assert
        Assert.That(result, Is.Null);
    }

    [TestCaseSource(nameof(GetMovieAsyncTestCaseData))]
    public async Task Should_ReturnExpectedMovie_WhenMovieExists(repo.Movie expectedMovieDao, List<repo.Movie> movies)
    {
        // Arrange
        InsertMovies(movies);

        // Act
        var result = await GetRepository().GetMovieAsync(expectedMovieDao.Id);

        // Assert
        var expectedMovie = new dto.Movie
        {
            Id = expectedMovieDao.Id,
            Title = expectedMovieDao.Title,
            YearOfRelease = expectedMovieDao.YearOfRelease,
            RunningTime = expectedMovieDao.RunningTime,
            Genre = (Genres)expectedMovieDao.GenreId,
            AverageRating = expectedMovieDao.AverageRating
        };

        Assert.That(result, Is.Not.Null);
        StringAssert.AreEqualIgnoringCase(JsonConvert.SerializeObject(expectedMovie),
            JsonConvert.SerializeObject(result));
    }
}