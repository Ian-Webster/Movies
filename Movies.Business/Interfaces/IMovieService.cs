using System;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface IMovieService
    {
        /// <summary>
        /// Gets a movie by the given id
        /// </summary>
        /// <param name="movieId">id of the movie to get</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Movie> GetMovie(Guid movieId, CancellationToken token);

        /// <summary>
        /// Gets all movies
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Movie>> GetMovies(CancellationToken token);

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
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Movie>?> SearchMovies(MovieSearchCriteria movieSearchCriteria, CancellationToken token);

        /// <summary>
        /// Top movies by average rating
        /// </summary>
        /// <param name="movieCount"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Movie>> TopMovies(byte movieCount, CancellationToken token);

        /// <summary>
        /// Top movies for the given user
        /// </summary>
        /// <param name="movieCount"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Movie>?> TopMoviesByUser(byte movieCount, Guid userId, CancellationToken token);

        /// <summary>
        /// Checks if the given movie exists
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> MovieExists(Guid movieId, CancellationToken token);

        /// <summary>
        /// Saves the given movie, if existing movie updates, if new inserts, returns true if save was successful
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> SaveMovie(Movie movie, CancellationToken token);
    }
}
