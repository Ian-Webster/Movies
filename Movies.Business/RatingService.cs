using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using Movies.Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Business
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public RatingService(IRatingRepository ratingRepository, IMovieService movieService, IUserService userService)
        {
            _ratingRepository = ratingRepository;
            _movieService = movieService;
            _userService = userService;
        }

        public async Task<bool> SaveRating(MovieRating movieRating, CancellationToken token)
        {
            var validationResult = await ValidateMovieRating(movieRating, token);

            switch(validationResult)
            {
                case MovieRatingSaveValidationResults.NullRating:
                    throw new ArgumentException("movieRating must not be null", nameof(movieRating));
                case MovieRatingSaveValidationResults.InvalidMovieId:
                    throw new ArgumentException("movieRating.MovieId must be greater than zero", nameof(movieRating));
                case MovieRatingSaveValidationResults.MovieNotfound:
                    throw new ArgumentException($"Move not found for movieRating.MovieId {movieRating.MovieId} not found", nameof(movieRating));
                case MovieRatingSaveValidationResults.InvalidUserId:
                    throw new ArgumentException("movieRating.UserId must be greater than zero", nameof(movieRating));
                case MovieRatingSaveValidationResults.UserNotFound:
                    throw new ArgumentException($"User note found for movieRating.UserId {movieRating.UserId} must not be zero", nameof(movieRating));
            }

            return await _ratingRepository.SaveRating(movieRating, token);
        }

        public async Task<MovieRatingSaveValidationResults> ValidateMovieRating(MovieRating movieRating, CancellationToken token)
        {
            if (movieRating == null)
            {
                return MovieRatingSaveValidationResults.NullRating;
            }

            if (movieRating.MovieId == Guid.Empty)
            {
                return MovieRatingSaveValidationResults.InvalidMovieId;
            }

            if (!await _movieService.MovieExists(movieRating.MovieId, token))
            {
                return MovieRatingSaveValidationResults.MovieNotfound;
            }

            if (movieRating.UserId == Guid.Empty)
            {
                return MovieRatingSaveValidationResults.InvalidUserId;
            }

            if (! await _userService.UserExists(movieRating.UserId, token))
            {
                return MovieRatingSaveValidationResults.UserNotFound;
            }

            return MovieRatingSaveValidationResults.OK;

        }
    }
}
