using Movies.Domain.Enums;
using System;

namespace Movies.Domain.DTO
{
    /// <summary>
    /// Movie
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Id of movie
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Movies title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Year the movie was released
        /// </summary>
        public short YearOfRelease { get; set; }
        /// <summary>
        /// Movies genre
        /// </summary>
        public Genres Genre { get; set; }
        /// <summary>
        /// Running time of movie (in minutes)
        /// </summary>
        public byte RunningTime { get; set; }
        /// <summary>
        /// Average user rating for the movie
        /// </summary>
        public decimal AverageRating { get; set; }
        /// <summary>
        /// AverageRating field rounded to the nearest 0.5 for display in the UI
        /// </summary>
        public decimal AverageRatingForDisplay
        {
            get
            {
                if (AverageRating == 0)
                {
                    return 0m;
                }
                return Math.Round(AverageRating * 2, MidpointRounding.AwayFromZero) / 2;
            }
        }
    }
}
