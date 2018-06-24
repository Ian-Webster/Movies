using System.Collections.Generic;

namespace Movies.Domain.DTO
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Users id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List of moving ratings submitted by the user
        /// </summary>
        public List<MovieRating> MovieRatings { get; set; }
    }
}
