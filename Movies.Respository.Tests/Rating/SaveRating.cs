using System;
using Movies.Domain.DTO;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Repo = Movies.Repository.Entities;

namespace Movies.Repository.Tests.Rating
{
    [TestFixture]
    public class SaveRating : RatingRepositoryBase
    {

        [Test]
        public async Task Should_AddNewRatingToDatabase()
        {
            // Arrange
            var rating = new MovieRating { MovieId = Guid.NewGuid(), Rating = 2, UserId = Guid.NewGuid() };

            // Act
            await GetRepository().SaveRatingAsync(rating);

            var ratingCount = 0;
            var firstRating = new Repo.MovieRating();

            using (var context = GetContext())
            {
                ratingCount = context.MovieRatingDbSet.Count();
                firstRating = context.MovieRatingDbSet.First();
            }

            // Assert
            Assert.That(1, Is.EqualTo(ratingCount));

            Assert.That(firstRating, Is.Not.Null);
            Assert.That(rating.MovieId, Is.EqualTo(firstRating.MovieId));
            Assert.That(rating.Rating, Is.EqualTo(firstRating.Rating));
            Assert.That(rating.UserId, Is.EqualTo(firstRating.UserId));
        }

        [Test]
        public async Task Should_UpdateExistingRatingInDatabase()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var existingRating = new Repo.MovieRating { MovieId = Guid.NewGuid(), UserId = userId, Rating = 3 };
            var newRating = new MovieRating { MovieId = existingRating.MovieId, UserId = userId, Rating = 4 };

            InsertRating(existingRating);

            // Act
            await GetRepository().SaveRatingAsync(newRating);

            var ratingCount = 0;
            var firstRating = new Repo.MovieRating();

            using (var context = GetContext())
            {
                ratingCount = context.MovieRatingDbSet.Count();
                firstRating = context.MovieRatingDbSet.First();
            }

            // Assert
            Assert.That(1, Is.EqualTo(ratingCount));

            Assert.That(firstRating, Is.Not.Null);
            Assert.That(existingRating.MovieId, Is.EqualTo(firstRating.MovieId));
            Assert.That(newRating.Rating, Is.EqualTo(firstRating.Rating));
            Assert.That(existingRating.UserId, Is.EqualTo(firstRating.UserId));
        }

    }
}
