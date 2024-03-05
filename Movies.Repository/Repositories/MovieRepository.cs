using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.DTO;
using Movies.Domain.Enums;
using Movies.Repository.Interfaces;
using Movie = Movies.Domain.DTO.Movie;
using Repo = Movies.Repository.Entities;

namespace Movies.Repository.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly UnitOfWork<Context> _unitOfWork;
        private readonly IRepository<Repo.Movie> _movieRepository;
        private readonly IRepository<Repo.MovieRating> _movieRatingRepository;

        public MovieRepository(UnitOfWork<Context> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _movieRepository = _unitOfWork.Repository<Repo.Movie>();
            _movieRatingRepository = _unitOfWork.Repository<Repo.MovieRating>();
        }

        public async Task<Movie?> GetMovie(Guid movieId, CancellationToken token)
        {
            return await _movieRepository.FirstOrDefaultProjected(m => m.Id == movieId, 
                _movieProjection,
                token);
        }

        public async Task<IEnumerable<Movie>?> GetMoviesAsync(CancellationToken token)
        {
            return await _movieRepository.ListProjected(m => true, _movieProjection, token);
        }

        public async Task<bool> MovieExists(Guid movieId, CancellationToken token)
        {
            return await _movieRepository.Exists(m => m.Id == movieId, token);
        }

        public async Task<IEnumerable<Movie>?> SearchMovies(MovieSearchCriteria movieSearchCriteria, CancellationToken token)
        {
            return await _movieRepository.ListProjected(
                m => (string.IsNullOrWhiteSpace(movieSearchCriteria.Title) ||
                      m.Title.Contains(movieSearchCriteria.Title)) &&
                     (movieSearchCriteria.YearOfRelease == 0 || m.YearOfRelease == movieSearchCriteria.YearOfRelease) &&
                     (movieSearchCriteria.Genres == null || movieSearchCriteria.Genres.Contains((Genres)m.GenreId)),
                _movieProjection, 
                token);
        }

        public async Task<IEnumerable<Movie>> TopMovies(byte movieCount, CancellationToken token)
        {
            return await _movieRepository.DbSet
                .Where(m => true)
                .OrderByDescending(m => m.AverageRating)
                .ThenBy(m => m.Title)
                .Select(_movieProjection)
                .Take(movieCount)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Movie>> TopMoviesByUser(byte movieCount, Guid userId, CancellationToken token)
        {
            return await _movieRatingRepository.DbSet
                .Where(mr => mr.UserId == userId)
                .OrderByDescending(mr => mr.Rating)
                .ThenBy(mr => mr.Movie.Title)
                .Take(movieCount)
                .Select(mr => new Movie
                {
                    AverageRating = mr.Rating,
                    Genre = (Genres)mr.Movie.GenreId,
                    Id = mr.Movie.Id,
                    RunningTime = mr.Movie.RunningTime,
                    Title = mr.Movie.Title,
                    YearOfRelease = mr.Movie.YearOfRelease
                }).ToListAsync(token);
        }

        public async Task<bool> SaveMovie(Movie movie, CancellationToken token)
        {
            var movieDao = movie.Id != Guid.Empty ? await _movieRepository.FirstOrDefault(m => m.Id == movie.Id, token): null;

            if (movieDao == null)
            {
                movieDao = new Repo.Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    GenreId = (short)movie.Genre,
                    YearOfRelease = movie.YearOfRelease,
                    RunningTime = movie.RunningTime
                };
                if (!await _movieRepository.Add(movieDao, token)) return false;
            }
            else
            {
                movieDao.GenreId = (short)movie.Genre;
                movieDao.RunningTime = movie.RunningTime;
                movieDao.Title = movie.Title;
                movieDao.YearOfRelease = movie.YearOfRelease;
            }
            //TODO: reverse this once https://github.com/Ian-Webster/DataAccess/issues/30 is fixed
            //return await _unitOfWork.Save(token);
            await _unitOfWork.Save(token);
            return true;
        }

        private readonly Expression<Func<Repo.Movie, Movie>> _movieProjection = m => new Movie
        {
            AverageRating = m.AverageRating,
            Genre = (Genres)m.GenreId,
            Id = m.Id,
            RunningTime = m.RunningTime,
            Title = m.Title,
            YearOfRelease = m.YearOfRelease
        };

    }
}
