using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.DTO
{
    /// <summary>
    /// Users movie rating
    /// </summary>
    public class MovieRating
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Id of the movie
        /// </summary>
        [Required]
        public int MovieId { get; set; }
        /// <summary>
        /// User rating for the movie, must be zero or greater
        /// </summary>
        [Required]
        public byte Rating { get; set; }
    }
}
