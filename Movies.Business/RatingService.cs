using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using Movies.Repository.Interfaces;
using System;

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

        public bool SaveRating(MovieRating movieRating)
        {
            var validationResult = ValidateMovieRating(movieRating);

            switch(validationResult)
            {
                case MovieRatingSaveValidationResults.NullRating:
                    throw new ArgumentException("movieRating must not be null", "movieRating");
                case MovieRatingSaveValidationResults.InvalidMovieId:
                    throw new ArgumentException("movieRating.MovieId must be greater than zero", "movieRating");
                case MovieRatingSaveValidationResults.MovieNotfound:
                    throw new ArgumentException($"Move not found for movieRating.MovieId {movieRating.MovieId} not found", "movieRating");
                case MovieRatingSaveValidationResults.InvalidUserId:
                    throw new ArgumentException("movieRating.UserId must be greater than zero", "movieRating");
                case MovieRatingSaveValidationResults.UserNotFound:
                    throw new ArgumentException($"User note found for movieRating.UserId {movieRating.UserId} must not be zero", "movieRating");
            }

            return _ratingRepository.SaveRating(movieRating);
        }

        public MovieRatingSaveValidationResults ValidateMovieRating(MovieRating movieRating)
        {
            if (movieRating == null)
            {
                return MovieRatingSaveValidationResults.NullRating;
            }

            if (movieRating.MovieId <= 0)
            {
                return MovieRatingSaveValidationResults.InvalidMovieId;
            }

            if (!_movieService.MovieExists(movieRating.MovieId))
            {
                return MovieRatingSaveValidationResults.MovieNotfound;
            }

            if (movieRating.UserId <= 0)
            {
                return MovieRatingSaveValidationResults.InvalidUserId;
            }

            if (!_userService.UserExists(movieRating.UserId))
            {
                return MovieRatingSaveValidationResults.UserNotFound;
            }

            return MovieRatingSaveValidationResults.OK;

        }
    }
}
