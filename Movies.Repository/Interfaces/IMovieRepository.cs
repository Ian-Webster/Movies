using Movies.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repository.Interfaces
{
    public interface IMovieRepository 
    {
        /// <summary>
        /// Checks if the given movie exists
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        Task<bool> MovieExistsAsync(int movieId);

        /// <summary>
        /// Searches movies using the given criteria
        /// </summary>
        /// <param name="movieSearchCritera"></param>
        /// <returns></returns>
        Task<List<Movie>> SearchMoviesAsync(MovieSearchCriteria movieSearchCritera);

        /// <summary>
        /// Returns the top X movies based on average user rating
        /// </summary>
        /// <param name="movieCount"></param>
        /// <returns></returns>
        Task<List<Movie>> TopMoviesAsync(byte movieCount);

        /// <summary>
        /// Returns the top X movies for the given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Movie>> TopMoviesByUserAsync(byte movieCount, int userId);

        /// <summary>
        /// Saves the movie, inserts if the movie doesn't exist or updates if the movie does exist
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        Task<bool> SaveMovieAsync(Movie movie);
    }
}
