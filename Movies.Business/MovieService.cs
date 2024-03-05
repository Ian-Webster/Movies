using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Business
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserService _userService;

        public MovieService(IMovieRepository movieRepository, IUserService userService)
        {
            _movieRepository = movieRepository;
            _userService = userService;
        }

        public async Task<Movie> GetMovie(Guid movieId, CancellationToken token)
        {
            return await _movieRepository.GetMovie(movieId, token);
        }

        public async Task<IEnumerable<Movie>> GetMovies(CancellationToken token)
        {
            return await _movieRepository.GetMoviesAsync(token);
        }

        public MovieSearchValidationResults ValidateSearchCriteria(MovieSearchCriteria movieSearchCriteria)
        {
            if (movieSearchCriteria == null)
            {
                return MovieSearchValidationResults.NoCriteria;
            }

            if (string.IsNullOrEmpty(movieSearchCriteria.Title)
                && movieSearchCriteria.YearOfRelease == 0
                && (movieSearchCriteria.Genres == null || !movieSearchCriteria.Genres.Any()))
            {
                return MovieSearchValidationResults.InvalidCriteria;
            }

            return MovieSearchValidationResults.OK;
        }

        public async Task<IEnumerable<Movie>?> SearchMovies(MovieSearchCriteria movieSearchCriteria, CancellationToken token)
        {
            var validationResult = ValidateSearchCriteria(movieSearchCriteria);
            if (validationResult == MovieSearchValidationResults.NoCriteria)
            {
                throw new ArgumentException("movieSearchCriteria must not be null", nameof(movieSearchCriteria));
            }

            if (validationResult == MovieSearchValidationResults.InvalidCriteria)
            {
                throw new ArgumentException("movieSearchCriteria must have at least one search criteria", nameof(movieSearchCriteria));
            }

            return await _movieRepository.SearchMovies(movieSearchCriteria, token);
        }

        public async Task<IEnumerable<Movie>> TopMovies(byte movieCount, CancellationToken token)
        {
            if (movieCount == 0)
            {
                throw new ArgumentException("movieCount must be greater than zero", nameof(movieCount));
            }

            return await _movieRepository.TopMovies(movieCount, token);
        }

        public async Task<IEnumerable<Movie>?> TopMoviesByUser(byte movieCount, Guid userId, CancellationToken token)
        {
            if (movieCount == 0)
            {
                throw new ArgumentException("movieCount must be greater than zero", nameof(movieCount));
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("userId cannot be empty", nameof(userId));
            }

            if (!await _userService.UserExists(userId, token))
            {
                throw new ArgumentException($"User not found for userId {userId}", nameof(userId));
            }

            return await _movieRepository.TopMoviesByUser(movieCount, userId,token);
        }

        public async Task<bool> MovieExists(Guid movieId, CancellationToken token)
        {
            if (movieId == Guid.Empty)
            {
                throw new ArgumentException("moveId cannot be empty", nameof(movieId));
            }

            return await _movieRepository.MovieExists(movieId, token);
        }

        public async Task<bool> SaveMovie(Movie movie, CancellationToken token)
        {
            return await _movieRepository.SaveMovie(movie, token);
        }
       
    }
}
