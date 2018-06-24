namespace Movies.Repository.Entities
{
    public class MovieRating
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public byte Rating { get; set; }

        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}
