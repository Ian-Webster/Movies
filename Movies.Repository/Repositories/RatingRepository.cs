using System.Threading;
using System.Threading.Tasks;
using DataAccess.Repository;
using Movies.Repository.Interfaces;
using MovieRating = Movies.Domain.DTO.MovieRating;
using Repo = Movies.Repository.Entities;

namespace Movies.Repository.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly UnitOfWork<Context> _unitOfWork;
        private readonly IRepository<Repo.MovieRating> _ratingRepository;

        public RatingRepository(UnitOfWork<Context> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ratingRepository = _unitOfWork.Repository<Repo.MovieRating>();
        }

        public async Task<bool> SaveRating(MovieRating movieRating, CancellationToken token)
        {
            var rating = await _ratingRepository.FirstOrDefault(mr => mr.MovieId == movieRating.MovieId && mr.UserId == movieRating.UserId, token);

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

              if (! await _ratingRepository.Add(rating, token)) return false;
            }

            //TODO: reverse this once https://github.com/Ian-Webster/DataAccess/issues/30 is fixed
            //return await _unitOfWork.Save(token);
            await _unitOfWork.Save(token);
            return true;
        }

    }
}
