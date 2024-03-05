using System;
using Movies.Domain.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Repository.Interfaces
{
    public interface IMovieRepository 
    {
        /// <summary>
        /// Gets a movie by the given movieId
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Movie?> GetMovie(Guid movieId, CancellationToken token);

        /// <summary>
        /// Gets all movies
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Movie>?> GetMoviesAsync(CancellationToken token);

        /// <summary>
        /// Checks if the given movie exists
        /// </summary>
        /// <param name="movieId">id of the movie to check</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> MovieExists(Guid movieId, CancellationToken token);

        /// <summary>
        /// Searches movies using the given criteria
        /// </summary>
        /// <param name="movieSearchCriteria">the search criteria</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Movie>?> SearchMovies(MovieSearchCriteria movieSearchCriteria, CancellationToken token);

        /// <summary>
        /// Returns the top X movies based on average user rating
        /// </summary>
        /// <param name="movieCount">number of movies to get</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Movie>> TopMovies(byte movieCount, CancellationToken token);

        /// <summary>
        /// Returns the top X movies for the given user
        /// </summary>
        /// <param name="movieCount">number of movies to get</param>
        /// <param name="userId">id of the user to get movies for</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Movie>?> TopMoviesByUser(byte movieCount, Guid userId, CancellationToken token);

        /// <summary>
        /// Saves the movie, inserts if the movie doesn't exist or updates if the movie does exist
        /// </summary>
        /// <param name="movie">movie to save</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> SaveMovie(Movie movie, CancellationToken token);
    }
}
