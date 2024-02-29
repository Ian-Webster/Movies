using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using repo = Movies.Repository.Entities;

namespace Movies.Repository.Tests.Movie
{
    [TestFixture]
    public class TopMovies: MovieRepositoryBase
    {

        [TestCase(1)]
        [TestCase(5)]
        public void Should_ReturnExpectedNumberOfResults(byte count)
        {
            //arrange
            UpdateTestMovieRatings();          

            List<repo.Movie> movies;

            using (var context = GetContext())
            {
                movies = context.MovieDbSet.OrderByDescending(m => m.AverageRating).Take(count).ToList();
            }

            //act
            var asyncResult = GetRepository().TopMoviesAsync(count);

            //assert
            var result = asyncResult.Result;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(result.Count));

            var minExpectedRating = movies.Min(m => m.AverageRating);
            var maxExpectedRating = movies.Max(m => m.AverageRating);

            Assert.That(minExpectedRating, Is.EqualTo(result.Min(m => m.AverageRating)));
            Assert.That(maxExpectedRating, Is.EqualTo(result.Max(m => m.AverageRating)));
        }

    }
}
