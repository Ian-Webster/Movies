using System;

namespace Movies.Repository.Entities
{
    public class MovieRating
    {
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
        public byte Rating { get; set; }

        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}
