namespace Movies.Domain.Enums.Validation
{
    /// <summary>
    /// Used to validate whether a movie rating can be saved or not
    /// </summary>
    public enum MovieRatingSaveValidationResults
    {
        /// <summary>
        /// Ok to save
        /// </summary>
        OK = 1,
        /// <summary>
        /// Null MovieRating object
        /// </summary>
        NullRating = 2,
        /// <summary>
        /// Negative or zero user id
        /// </summary>
        InvalidUserId = 3,
        /// <summary>
        /// User not found for given user id
        /// </summary>
        UserNotFound = 4,
        /// <summary>
        /// Negative or zero movie id
        /// </summary>
        InvalidMovieId = 5,
        /// <summary>
        /// Movie not found for given movie id
        /// </summary>
        MovieNotfound = 6
    }
}