using NUnit.Framework;
using dto = Movies.Domain.DTO;
using repo = Movies.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
            var newMovie = new dto.Movie { Genre = Genres.Action, RunningTime = 100, Title = "test movie", YearOfRelease = 1999 };

            var existingMovieCount = 0;

            using (var context = GetContext())
            {
                existingMovieCount = context.MovieDbSet.Count();
            }

            //act
            var result = GetRepository().SaveMovieAsync(newMovie);

            //assert
            var newMovieCount = 0;
            repo.Movie lastMovie = null;
            using (var context = GetContext())
            {
                newMovieCount = context.MovieDbSet.Count();
                lastMovie = context.MovieDbSet.OrderBy(m => m.Id).Last();
            }

            Assert.AreEqual(existingMovieCount + 1, newMovieCount);
            Assert.AreEqual(newMovie.Genre, (Genres)lastMovie.GenreId);
            Assert.AreEqual(newMovie.RunningTime, lastMovie.RunningTime);
            Assert.AreEqual(newMovie.Title, lastMovie.Title);
            Assert.AreEqual(newMovie.YearOfRelease, lastMovie.YearOfRelease);
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

            Assert.AreEqual(existingMovieCount, newMovieCount);            
            Assert.AreEqual(updatedMovie.Genre, (Genres)updatedExistingMovie.GenreId);
            Assert.AreEqual(updatedMovie.RunningTime, updatedExistingMovie.RunningTime);
            Assert.AreEqual(updatedMovie.Title, updatedExistingMovie.Title);
            Assert.AreEqual(updatedMovie.YearOfRelease, updatedExistingMovie.YearOfRelease);
        }

    }
}
