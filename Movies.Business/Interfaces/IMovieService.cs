using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using System.Collections.Generic;

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
        List<Movie> SearchMovies(MovieSearchCriteria movieSearchCriteria);

        /// <summary>
        /// Top movies by average rating
        /// </summary>
        /// <param name="movieCount"></param>
        /// <returns></returns>
        List<Movie> TopMovies(byte movieCount);

        /// <summary>
        /// Top movies for the given user
        /// </summary>
        /// <param name="movieCount"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Movie> TopMoviesByUser(byte movieCount, int userId);

        /// <summary>
        /// Checks if the given movie exists
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        bool MovieExists(int movieId);
    }
}
