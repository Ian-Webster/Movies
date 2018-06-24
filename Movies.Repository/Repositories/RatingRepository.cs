using Movies.Domain.DTO;
using Movies.Repository.Interfaces;
using System.Linq;
using Repo = Movies.Repository.Entities;

namespace Movies.Repository
{
    public class RatingRepository : BaseRepository, IRatingRepository
    {
        public RatingRepository(Context context) : base(context) { }

        public bool SaveRating(MovieRating movieRating)
        {
            var rating = _context.MovieRatingDbSet.FirstOrDefault(mr => mr.MovieId == movieRating.MovieId && mr.UserId == movieRating.UserId);

            if (rating != null)
            {
                rating.Rating = movieRating.Rating;
            }
            else
            {
                rating = new Repo.MovieRating
                {
                    MovieId = movieRating.MovieId,
                    UserId = movieRating.UserId,
                    Rating = movieRating.Rating
                };

                _context.MovieRatingDbSet.Add(rating);
            }

            return _context.SaveChanges() > 0;
        }

    }
}
