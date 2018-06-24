namespace Movies.Domain.Enums.Validation
{
    /// <summary>
    /// Used to validate if a search criteria can be submitted or not
    /// </summary>
    public enum MovieSearchValidationResults
    {
        /// <summary>
        /// OK to submit search
        /// </summary>
        OK = 1,
        /// <summary>
        /// Null search criteria object
        /// </summary>
        NoCriteria = 2,
        /// <summary>
        /// No search criteria set on search criteria object
        /// </summary>
        InvalidCriteria = 3
    }
}