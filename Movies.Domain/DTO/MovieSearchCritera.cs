using Movies.Domain.Enums;
using System.Collections.Generic;

namespace Movies.Domain.DTO
{
    /// <summary>
    /// Movie search criteria, used in movie searches.
    /// At least one field must be completed to search
    /// </summary>
    public class MovieSearchCriteria
    {
        /// <summary>
        /// Movie title to search by, can be partial
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Year of movie release to search by
        /// </summary>
        public short YearOfRelease { get; set; }
        /// <summary>
        /// List of genres to search by
        /// </summary>
        public List<Genres> Genres { get; set; }
    }
}
