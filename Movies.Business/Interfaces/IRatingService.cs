using System.Threading;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface IRatingService
    {
        /// <summary>
        /// Validates the movieRating for saving
        /// </summary>
        /// <param name="movieRating"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<MovieRatingSaveValidationResults> ValidateMovieRating(MovieRating movieRating, CancellationToken token);

        /// <summary>
        /// Saves the rating.
        /// If the rating already exists it will be updated to the new value
        /// </summary>
        /// <param name="movieRating"></param>
        /// <param name="token"></param>
        /// <returns>MovingRatingSaveResults indicate save outcome</returns>
        Task<bool> SaveRating(MovieRating movieRating, CancellationToken token);
    }
}
