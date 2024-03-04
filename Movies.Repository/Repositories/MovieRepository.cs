using System;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.DTO;
using Movies.Domain.Enums;
using Movies.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo = Movies.Repository.Entities;

namespace Movies.Repository
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(Context context) : base(context) { }

        public async Task<bool> MovieExistsAsync(Guid movieId)
        {
            return await _context.MovieDbSet.AnyAsync(m => m.Id == movieId);
        }

        public async Task<List<Movie>> SearchMoviesAsync(MovieSearchCriteria movieSearchCritera)
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

            return await query.Select(m => new Movie
                {
                    AverageRating = m.AverageRating,
                    Genre = (Genres)m.GenreId,
                    Id = m.Id,
                    RunningTime = m.RunningTime,
                    Title = m.Title,
                    YearOfRelease = m.YearOfRelease
                })
                .ToListAsync();
        }

        public async Task<List<Movie>> TopMoviesAsync(byte movieCount)
        {
            return await _context.MovieDbSet
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
                .ToListAsync();
        }
        
        public async Task<List<Movie>> TopMoviesByUserAsync(byte movieCount, Guid userId)
        {
            var movieRatings = _context.MovieRatingDbSet
                .Where(mr => mr.UserId == userId)
                .OrderByDescending(mr => mr.Rating)
                .ThenBy(mr => mr.Movie.Title)
                .Take(movieCount);

            return await movieRatings.Select(mr => new Movie
            {
                AverageRating = mr.Rating,
                Genre = (Genres)mr.Movie.GenreId,
                Id = mr.Movie.Id,
                RunningTime = mr.Movie.RunningTime,
                Title = mr.Movie.Title,
                YearOfRelease = mr.Movie.YearOfRelease
            }).ToListAsync();

        }

        public async Task<bool> SaveMovieAsync(Movie movie)
        {
            var movieDao = movie.Id != Guid.Empty ? await _context.MovieDbSet.FindAsync(movie.Id): null;
            if (movieDao == null)
            {
                movieDao = new Repo.Movie
                {
                    Id = movie.Id
                };
                _context.Add(movieDao);
            }
            movieDao.GenreId = (short)movie.Genre;
            movieDao.RunningTime = movie.RunningTime;
            movieDao.Title = movie.Title;
            movieDao.YearOfRelease = movie.YearOfRelease;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Movie> GetMovieAsync(Guid movieId)
        {
            var result = _context.MovieDbSet
                .Where(m => m.Id == movieId)
                .Select(m => new Movie
                {
                    AverageRating = m.AverageRating,
                    Genre = (Genres)m.GenreId,
                    Id = m.Id,
                    RunningTime = m.RunningTime,
                    Title = m.Title,
                    YearOfRelease = m.YearOfRelease
                });

            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await _context.MovieDbSet
                .OrderBy(m => m.Title)
                .Select(m => new Movie
                {
                    AverageRating = m.AverageRating,
                    Genre = (Genres)m.GenreId,
                    Id = m.Id,
                    RunningTime = m.RunningTime,
                    Title = m.Title,
                    YearOfRelease = m.YearOfRelease
                })
                .ToListAsync();
        }
    }
}
