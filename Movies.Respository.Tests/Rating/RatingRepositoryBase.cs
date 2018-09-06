using Movies.Repository.Entities;

namespace Movies.Repository.Tests.Rating
{
    public class RatingRepositoryBase: TestRepositoryBase
    {

        protected RatingRepository GetRepository()
        {
            return new RatingRepository(new Context(options.Options));
        }

        protected void InsertRating(MovieRating rating)
        {
            using (var context = GetContext())
            {
                context.Add(rating);
                context.SaveChanges();
            }
        }

    }
}
