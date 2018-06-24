using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;

namespace Movies.Business.Interfaces
{
    public interface IRatingService
    {
        /// <summary>
        /// Validates the movieRating for saving
        /// </summary>
        /// <param name="movieRating"></param>
        /// <returns></returns>
        MovieRatingSaveValidationResults ValidateMovieRating(MovieRating movieRating);

        /// <summary>
        /// Saves the rating.
        /// If the rating already exists it will be updated to the new value
        /// </summary>
        /// <param name="movieRating"></param>
        /// <returns>MovingRatingSaveResults indicate save outcome</returns>
        bool SaveRating(MovieRating movieRating);

    }
}
