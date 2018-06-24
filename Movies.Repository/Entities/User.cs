using System.Collections.Generic;

namespace Movies.Repository.Entities
{
    public class User
    {
        public int Id { get; set; }

        public IEnumerable<MovieRating> MovieRatings { get; set; }
    }
}
