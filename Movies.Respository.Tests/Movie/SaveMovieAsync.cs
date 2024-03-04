using System;
using NUnit.Framework;
using dto = Movies.Domain.DTO;
using repo = Movies.Repository.Entities;
using Movies.Domain.Enums;
using System.Linq;

namespace Movies.Repository.Tests.Movie
{
    [TestFixture]
    public class SaveMovieAsync : MovieRepositoryBase
    {
        [Test]
        public void Should_AddNewMovies()
        {
            //arrange
            var newMovie = new dto.Movie { Id = Guid.NewGuid(), Genre = Genres.Action, RunningTime = 100, Title = "test movie", YearOfRelease = 1999 };

            var existingMovieCount = 0;

            using (var context = GetContext())
            {
                existingMovieCount = context.MovieDbSet.Count();
            }

            //act
            var result = GetRepository().SaveMovieAsync(newMovie);

            //assert
            var newMovieCount = 0;
            repo.Movie insertedMovie = null;
            using (var context = GetContext())
            {
                newMovieCount = context.MovieDbSet.Count();
                insertedMovie = context.MovieDbSet.Find(newMovie.Id);
            }

            Assert.That(existingMovieCount + 1, Is.EqualTo(newMovieCount));
            Assert.That(newMovie.Genre, Is.EqualTo((Genres)insertedMovie.GenreId));
            Assert.That(newMovie.RunningTime, Is.EqualTo(insertedMovie.RunningTime));
            Assert.That(newMovie.Title, Is.EqualTo(insertedMovie.Title));
            Assert.That(newMovie.YearOfRelease, Is.EqualTo(insertedMovie.YearOfRelease));
        }

        [Test]
        public void Should_UpdateExistingMovie()
        {
            //arrange
            repo.Movie existingMovie = null;
            var existingMovieCount = 0;

            using (var context = GetContext())
            {
                existingMovie = context.MovieDbSet.First();
                existingMovieCount = context.MovieDbSet.Count();
            }

            var updatedMovie = new dto.Movie
            {
                Id = existingMovie.Id,
                Genre = Genres.ScienceFiction,
                RunningTime = (byte)(existingMovie.RunningTime + 15),
                Title = existingMovie.Title + " updated"
            };

            //act
            var result = GetRepository().SaveMovieAsync(updatedMovie);

            //assert
            var newMovieCount = 0;
            repo.Movie updatedExistingMovie = null;
            using (var context = GetContext())
            {
                newMovieCount = context.MovieDbSet.Count();
                updatedExistingMovie = context.MovieDbSet.Find(existingMovie.Id);
            }

            Assert.That(existingMovieCount, Is.EqualTo(newMovieCount));            
            Assert.That(updatedMovie.Genre, Is.EqualTo((Genres)updatedExistingMovie.GenreId));
            Assert.That(updatedMovie.RunningTime, Is.EqualTo(updatedExistingMovie.RunningTime));
            Assert.That(updatedMovie.Title, Is.EqualTo(updatedExistingMovie.Title));
            Assert.That(updatedMovie.YearOfRelease, Is.EqualTo(updatedExistingMovie.YearOfRelease));
        }

    }
}
