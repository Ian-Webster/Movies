using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> MovieExistsAsync(Guid movieId)
        {
            if (movieId == Guid.Empty)
            {
                throw new ArgumentException("moveId cannot be empty", nameof(movieId));
            }

            return await _movieRepository.MovieExistsAsync(movieId);
        }

        public async Task<List<Movie>> SearchMoviesAsync(MovieSearchCriteria movieSearchCriteria)
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

            return await _movieRepository.SearchMoviesAsync(movieSearchCriteria);
        }

        public async Task<List<Movie>> TopMoviesAsync(byte movieCount)
        {
            if (movieCount == 0)
            {
                throw new ArgumentException("movieCount must be greater than zero", nameof(movieCount));
            }

            return await _movieRepository.TopMoviesAsync(movieCount);
        }

        public async Task<List<Movie>> TopMoviesByUserAsync(byte movieCount, Guid userId)
        {
            if (movieCount == 0)
            {
                throw new ArgumentException("movieCount must be greater than zero", nameof(movieCount));
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("userId cannot be empty", nameof(userId));
            }

            if (! await _userService.UserExistsAsync(userId))
            {
                throw new ArgumentException($"User not found for userId {userId}", nameof(userId));
            }

            return await _movieRepository.TopMoviesByUserAsync(movieCount, userId);
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

        public async Task<bool> SaveMovieAsync(Movie movie)
        {
            return await _movieRepository.SaveMovieAsync(movie);
        }

        public async Task<Movie> GetMovieAsync(Guid movieId)
        {
            return await _movieRepository.GetMovieAsync(movieId);
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await _movieRepository.GetMoviesAsync();
        }
    }
}
