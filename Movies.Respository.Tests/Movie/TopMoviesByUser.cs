using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using repo = Movies.Repository.Entities;

namespace Movies.Repository.Tests.Movie
{
    [TestFixture]
    public class TopMoviesByUser: MovieRepositoryBase
    {
        [Ignore("bug in the in memory provider does not reset identities when a new context is created so Id's can't be guaranteed between tests see https://github.com/aspnet/EntityFrameworkCore/issues/6872")]
        [TestCase(1, 1)]
        [TestCase(2, 5)]
        [TestCase(1, 5)]
        public void Should_ReturnExpectedNumberOfResults(int userId, byte count)
        {
            //arrange
            UpdateTestMovieRatings();
            
            List<repo.Movie> movies;
            repo.User user;

            using (var context = GetContext())
            {
                movies = context.MovieDbSet.ToList();
                user = context.UserDbSet.Include(u => u.MovieRatings).First(u => u.Id == userId);
            }

            //act
            var asyncResult = GetRepository().TopMoviesByUserAsync(count, userId);

            //assert
            var result = asyncResult.Result;

            Assert.That(result, Is.Not.Null);

            if (user.MovieRatings.Count() > count)
            {
                Assert.That(result.Count(), Is.EqualTo(result.Count()));
            }
            else if (user.MovieRatings.Count() < count)
            {
                Assert.That(result.Count(), Is.EqualTo(user.MovieRatings.Count()));
            }
            else
            {
                Assert.That(count, Is.EqualTo(count));
            }            

            var minExpectedRating = user.MovieRatings.OrderByDescending(mr => mr.Rating).Take(count).Min(m => m.Rating);
            var maxExpectedRating = user.MovieRatings.OrderByDescending(mr => mr.Rating).Take(count).Max(m => m.Rating);

            Assert.That(result.Min(m => m.AverageRating), Is.EqualTo(minExpectedRating));
            Assert.That(result.Max(m => m.AverageRating), Is.EqualTo(maxExpectedRating));
        }

    }
}
