using System;
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
        public Guid Id { get; set; }

        /// <summary>
        /// The users username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The users password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// List of moving ratings submitted by the user
        /// </summary>
        public List<MovieRating> MovieRatings { get; set; }
    }
}
