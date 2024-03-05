using Movies.Domain.DTO;
using Movies.Domain.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Repo = Movies.Repository.Entities;

namespace Movies.Repository.Tests.Movie
{
    [TestFixture]
    public class SearchMovies : MovieRepositoryBase
    {
        [TestCase("action", 1)]
        [TestCase("abc", 3)]
        [TestCase("test", 4)]        
        public async Task Should_HonorMovieTitleFilter(string movieTitle, int expectedMovieCount)
        {
            // Arrange / Act
            var result = await GetRepository().SearchMovies(new MovieSearchCriteria { Title = movieTitle }, GetCancellationToken());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(expectedMovieCount, Is.EqualTo(result.Count()));
            Assert.That(expectedMovieCount, Is.EqualTo(result.Count(m => m.Title.Contains(movieTitle))));
        }

        
        [TestCase(2001, 1)]
        [TestCase(2000, 2)]
        [TestCase(1999, 4)]
        public async Task Should_HonorYearOfReleaseFilter(short yearOfrelease, int expectedMovieCount)
        {
            // Arrange / Act
            var result = await GetRepository().SearchMovies(new MovieSearchCriteria { YearOfRelease = yearOfrelease }, GetCancellationToken());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(expectedMovieCount, Is.EqualTo(result.Count()));
            Assert.That(expectedMovieCount, Is.EqualTo(result.Count(m => m.YearOfRelease == yearOfrelease)));
        }

        [Test, TestCaseSource(nameof(GenreData))]
        public async Task Should_HonorGenresFilter(List<Genres> genres, int expectedMovieCount)
        {
            // Arrange / Act
            var result = await GetRepository().SearchMovies(new MovieSearchCriteria { Genres = genres }, GetCancellationToken());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(expectedMovieCount, Is.EqualTo(result.Count()));
            Assert.That(expectedMovieCount, Is.EqualTo(result.Count(m => genres.Contains(m.Genre))));
        }

        [Ignore("Need to fix issues with equality test")]
        [Test, TestCaseSource(nameof(MixFilterData))]
        public async Task Should_HonorMixedFilter(MovieSearchCriteria searchCriteria, int expectedMovieCount)
        {
            // Arrange / Act
            var result = await GetRepository().SearchMovies(searchCriteria, GetCancellationToken());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(expectedMovieCount, Is.EqualTo(result.Count()));
            /*Assert.AreEqual(expectedMovieCount, result.Count(m => (string.IsNullOrEmpty(searchCriteria.Title) || m.Title.Contains(searchCriteria.Title)) 
                && (searchCriteria.YearOfRelease == 0 || m.YearOfRelease == searchCriteria.YearOfRelease) 
                && ( (searchCriteria.Genres == null || !searchCriteria.Genres.Any()) || searchCriteria.Genres.Contains(m.Genre) ) ));*/

        }

    }
}
