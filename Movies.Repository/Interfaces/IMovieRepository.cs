using Movies.Domain.DTO;
using System.Collections.Generic;

namespace Movies.Repository.Interfaces
{
    public interface IMovieRepository 
    {
        /// <summary>
        /// Checks if the given movie exists
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        bool MovieExists(int movieId);

        /// <summary>
        /// Searches movies using the given criteria
        /// </summary>
        /// <param name="movieSearchCritera"></param>
        /// <returns></returns>
        List<Movie> SearchMovies(MovieSearchCriteria movieSearchCritera);

        /// <summary>
        /// Returns the top X movies based on average user rating
        /// </summary>
        /// <param name="movieCount"></param>
        /// <returns></returns>
        List<Movie> TopMovies(byte movieCount);

        /// <summary>
        /// Returns the top X movies for the given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Movie> TopMoviesByUser(byte movieCount, int userId);
    }
}
