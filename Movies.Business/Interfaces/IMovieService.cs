using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface IMovieService
    {
        /// <summary>
        /// Validates the given movieSearchCriteria
        /// </summary>
        /// <param name="movieSearchCriteria"></param>
        /// <returns></returns>
        MovieSearchValidationResults ValidateSearchCriteria(MovieSearchCriteria movieSearchCriteria);

        /// <summary>
        /// Searches movies using the given criteria
        /// </summary>
        /// <param name="movieSearchCriteria"></param>
        /// <returns></returns>
        Task<List<Movie>> SearchMoviesAsync(MovieSearchCriteria movieSearchCriteria);

        /// <summary>
        /// Top movies by average rating
        /// </summary>
        /// <param name="movieCount"></param>
        /// <returns></returns>
        Task<List<Movie>> TopMoviesAsync(byte movieCount);

        /// <summary>
        /// Top movies for the given user
        /// </summary>
        /// <param name="movieCount"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Movie>> TopMoviesByUserAsync(byte movieCount, int userId);

        /// <summary>
        /// Checks if the given movie exists
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        Task<bool> MovieExistsAsync(int movieId);

        /// <summary>
        /// Saves the given movie, if existing movie updates, if new inserts, returns true if save was successful
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        Task<bool> SaveMovieAsync(Movie movie);
    }
}
