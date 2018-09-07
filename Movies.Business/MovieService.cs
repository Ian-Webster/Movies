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

        public async Task<bool> MovieExistsAsync(int movieId)
        {
            if (movieId <= 0)
            {
                throw new ArgumentException("moveId must be greater than zero", "movieId");
            }

            return await _movieRepository.MovieExistsAsync(movieId);
        }

        public async Task<List<Movie>> SearchMoviesAsync(MovieSearchCriteria movieSearchCriteria)
        {
            var validationResult = ValidateSearchCriteria(movieSearchCriteria);
            if (validationResult == MovieSearchValidationResults.NoCriteria)
            {
                throw new ArgumentException("movieSearchCriteria must not be null", "movieSearchCriteria");
            }

            if (validationResult == MovieSearchValidationResults.InvalidCriteria)
            {
                throw new ArgumentException("movieSearchCriteria must have at least one search criteria", "movieSearchCriteria");
            }

            return await _movieRepository.SearchMoviesAsync(movieSearchCriteria);
        }

        public async Task<List<Movie>> TopMoviesAsync(byte movieCount)
        {
            if (movieCount == 0)
            {
                throw new ArgumentException("movieCount must be greater than zero", "movieCount");
            }

            return await _movieRepository.TopMoviesAsync(movieCount);
        }

        public async Task<List<Movie>> TopMoviesByUserAsync(byte movieCount, int userId)
        {
            if (movieCount == 0)
            {
                throw new ArgumentException("movieCount must be greater than zero", "movieCount");
            }

            if (userId <= 0)
            {
                throw new ArgumentException("userId must be greater than zero", "userId");
            }

            if (! await _userService.UserExistsAsync(userId))
            {
                throw new ArgumentException($"User not found for userId {userId}", "userId");
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
    }
}
