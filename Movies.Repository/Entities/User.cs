using System;
using System.Collections.Generic;

namespace Movies.Repository.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public IEnumerable<MovieRating> MovieRatings { get; set; }
    }
}
