using Movies.Domain.DTO;
using NUnit.Framework;
using System.Linq;
using Repo = Movies.Repository.Entities;

namespace Movies.Repository.Tests.Rating
{
    [TestFixture]
    public class SaveRating : RatingRepositoryBase
    {

        [Test]
        public void Should_AddNewRatingToDatabase()
        {
            //arrange
            var rating = new MovieRating { MovieId = 1, Rating = 2, UserId = 3 };

            //act
            GetRepository().SaveRatingAsync(rating);

            var ratingCount = 0;
            var firstRating = new Repo.MovieRating();

            using (var context = GetContext())
            {
                ratingCount = context.MovieRatingDbSet.Count();
                firstRating = context.MovieRatingDbSet.First();
            }

            //assert
            Assert.That(1, Is.EqualTo(ratingCount));

            Assert.That(firstRating, Is.Not.Null);
            Assert.That(rating.MovieId, Is.EqualTo(firstRating.MovieId));
            Assert.That(rating.Rating, Is.EqualTo(firstRating.Rating));
            Assert.That(rating.UserId, Is.EqualTo(firstRating.UserId));
        }

        [Test]
        public void Should_UpdateExistingRatingInDatabase()
        {
            //arrange
            var existingRating = new Repo.MovieRating { MovieId = 1, UserId = 2, Rating = 3 };
            var newRating = new MovieRating { MovieId = 1, UserId = 2, Rating = 4 };

            InsertRating(existingRating);

            //act
            GetRepository().SaveRatingAsync(newRating);

            var ratingCount = 0;
            var firstRating = new Repo.MovieRating();

            using (var context = GetContext())
            {
                ratingCount = context.MovieRatingDbSet.Count();
                firstRating = context.MovieRatingDbSet.First();
            }

            //assert
            Assert.That(1, Is.EqualTo(ratingCount));

            Assert.That(firstRating, Is.Not.Null);
            Assert.That(existingRating.MovieId, Is.EqualTo(firstRating.MovieId));
            Assert.That(newRating.Rating, Is.EqualTo(firstRating.Rating));
            Assert.That(existingRating.UserId, Is.EqualTo(firstRating.UserId));
        }

    }
}
