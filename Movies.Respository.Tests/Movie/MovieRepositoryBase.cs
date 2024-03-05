using Movies.Domain.DTO;
using Movies.Domain.Enums;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Movies.Repository.Repositories;
using Movies.Repository.Tests.DataGeneration;
using Repo = Movies.Repository.Entities;

namespace Movies.Repository.Tests.Movie
{
    public class MovieRepositoryBase: TestRepositoryBase
    {

        [SetUp]
        protected new void Setup()
        {
            base.Setup();
            InsertMovieTestData();
            InsertUserRatingData();
        }

        protected MovieRepository GetRepository()
        {
            return new MovieRepository(GetUnitOfWork());
        }

        protected void UpdateTestMovieRatings()
        {
            using (var context = GetContext())
            {
                var movies = context.MovieDbSet.ToList();

                foreach (var movie in movies)
                {
                    movie.AverageRating = movies.IndexOf(movie);
                }

                context.SaveChanges();
            }
        }

        protected void InsertMovies(List<Repo.Movie> movies)
        {
            using (var context = GetContext())
            {
                context.AddRange(movies);
                context.SaveChanges();
            }
        }

        protected static IEnumerable GenreData()
        {
            TestCaseData result;

            var genres = new List<Genres> { Genres.Comedy };
            result = new TestCaseData(genres, 1);
            yield return result;

            genres = new List<Genres> { Genres.Action };
            result = new TestCaseData(genres, 4);
            yield return result;

            genres = new List<Genres> { Genres.Horror, Genres.ScienceFiction };
            result = new TestCaseData(genres, 2);
            yield return result;
        }

        protected static IEnumerable MixFilterData()
        {
            TestCaseData result;

            var filter = new MovieSearchCriteria { Genres = new List<Genres> { Genres.Action }, YearOfRelease = 1999 };
            result = new TestCaseData(filter, 4);
            yield return result;

            filter = new MovieSearchCriteria { Title = "test", YearOfRelease = 2000 };
            result = new TestCaseData(filter, 2);
            yield return result;

            filter = new MovieSearchCriteria { Title = "test", Genres = new List<Genres> { Genres.Horror } };
            result = new TestCaseData(filter, 1);
            yield return result;
        }

        protected static IEnumerable GetMovieAsyncTestCaseData()
        {
            var movies = MovieDataGeneration.GetRandomMovies(5);
            yield return new TestCaseData(
                movies.First(),
                movies).SetName($"{movies.Count} movies, id {movies.First().Id} expected");
            yield return new TestCaseData(
                movies.Last(),
                movies).SetName($"{movies.Count} movies, id {movies.Last().Id} expected");
            yield return new TestCaseData(
                movies.Skip(2).First(),
                movies).SetName($"{movies.Count} movies, id {movies.Skip(2).First().Id} expected");

            movies = MovieDataGeneration.GetRandomMovies(10);
            yield return new TestCaseData(
                movies.First(),
                movies).SetName($"{movies.Count} movies, id {movies.First().Id} expected");
            yield return new TestCaseData(
                movies.Last(),
                movies).SetName($"{movies.Count} movies, id {movies.Last().Id} expected");
            yield return new TestCaseData(
                movies.Skip(5).First(),
                movies).SetName($"{movies.Count} movies, id {movies.Skip(5).First().Id} expected");
        }

        private void InsertMovieTestData()
        {
            using(var context = GetContext())
            {
                context.Add(CreateMovie("action test", 1999, Genres.Action));
                context.Add(CreateMovie("comedy test", 2000, Genres.Comedy));
                context.Add(CreateMovie("horror test", 2000, Genres.Horror));
                context.Add(CreateMovie("science fiction test", 2001, Genres.ScienceFiction));

                context.Add(CreateMovie("abc 1", 1999, Genres.Action));
                context.Add(CreateMovie("abc 2", 1999, Genres.Action));
                context.Add(CreateMovie("abc 3", 1999, Genres.Action));

                context.SaveChanges();
            }
        }
        
        private Repo.Movie CreateMovie(string title, short yearOfRelease, Genres genre)
        {
            var rand = new Random();

            return new Repo.Movie
            {
                Title = title,
                YearOfRelease = yearOfRelease,
                GenreId = (short)genre,
                RunningTime = (byte)rand.Next(30, 180),
                AverageRating = rand.Next(1, 10)
            };
        }

        private void InsertUserRatingData()
        {
            using (var context = GetContext())
            {
                var movies = context.MovieDbSet.ToList();

                var user = new Repo.User();
                var ratings = new List<Repo.MovieRating>();

                movies.ForEach(m =>
                {
                    ratings.Add(new Repo.MovieRating { MovieId = m.Id, Rating = (byte)movies.IndexOf(m)});
                });
                user.MovieRatings = ratings;

                context.Add(user);

                user = new Repo.User();
                ratings = new List<Repo.MovieRating>();

                ratings.Add(new Repo.MovieRating { MovieId = movies.First().Id, Rating = 10 });
                ratings.Add(new Repo.MovieRating { MovieId = movies.Last().Id, Rating = 9 });

                context.Add(user);

                context.SaveChanges();
            }
        }

    }
}
