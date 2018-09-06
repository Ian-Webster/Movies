using Movies.Domain.DTO;
using Movies.Domain.Enums;
using Movies.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Repo = Movies.Repository.Entities;

namespace Movies.Repository
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(Context context) : base(context) { }

        public bool MovieExists(int movieId)
        {
            return _context.MovieDbSet.Any(m => m.Id == movieId);
        }

        public List<Movie> SearchMovies(MovieSearchCriteria movieSearchCritera)
        {
            IQueryable<Repo.Movie> query = _context.MovieDbSet
                .OrderBy(m => m.Title);

            if (!string.IsNullOrWhiteSpace(movieSearchCritera.Title))
            {
                query = query.Where(m => m.Title.Contains(movieSearchCritera.Title));
            }

            if (movieSearchCritera.YearOfRelease > 0)
            {
                query = query.Where(m => m.YearOfRelease == movieSearchCritera.YearOfRelease);
            }

            if (movieSearchCritera.Genres != null && movieSearchCritera.Genres.Any())
            {
                var genreList = movieSearchCritera.Genres.Cast<int>().ToList();
                query = query.Where(m => genreList.Contains(m.GenreId));
            }

            return query.Select(m => new Movie
                {
                    AverageRating = m.AverageRating,
                    Genre = (Genres)m.GenreId,
                    Id = m.Id,
                    RunningTime = m.RunningTime,
                    Title = m.Title,
                    YearOfRelease = m.YearOfRelease
                })
                .ToList();
        }

        public List<Movie> TopMovies(byte movieCount)
        {
            return _context.MovieDbSet
                .OrderByDescending(m => m.AverageRating)
                .ThenBy(m => m.Title)
                .Take(movieCount)
                .Select(m => new Movie
                    {
                        AverageRating = m.AverageRating,
                        Genre = (Genres)m.GenreId,
                        Id = m.Id,
                        RunningTime = m.RunningTime,
                        Title = m.Title,
                        YearOfRelease = m.YearOfRelease
                    })
                .ToList();
        }

        public List<Movie> TopMoviesByUser(byte movieCount, int userId)
        {
            var test = _context.MovieRatingDbSet
                .Where(mr => mr.UserId == userId)
                .OrderByDescending(mr => mr.Rating)
                .ThenBy(mr => mr.Movie.Title)
                .Take(movieCount);

            return test.Select(mr => new Movie
            {
                AverageRating = mr.Rating,
                Genre = (Genres)mr.Movie.GenreId,
                Id = mr.Movie.Id,
                RunningTime = mr.Movie.RunningTime,
                Title = mr.Movie.Title,
                YearOfRelease = mr.Movie.YearOfRelease
            }).ToList();

        }

    }
}
