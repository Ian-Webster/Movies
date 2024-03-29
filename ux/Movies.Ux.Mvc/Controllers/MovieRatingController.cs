﻿using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;

namespace Movies.Ux.Mvc.Controllers
{
    public class MovieRatingController : BaseController
    {
        private readonly IMovieService _movieService;
        private readonly IRatingService _ratingService;
        private readonly IUserService _userService;

        public MovieRatingController(IMovieService movieService, IRatingService ratingService, IUserService userService)
        {
            _movieService = movieService;
            _ratingService = ratingService;
            _userService = userService;
        }

        // GET: MovieRatingController/TopX/{movieCount}
        [Route("MovieRating/TopX/{movieCount:int}")]
        public async Task<ActionResult> TopX(byte movieCount)
        {
            var topMovies = await _movieService.TopMovies(movieCount, GetCancellationToken());
            await SetViewBagData(movieCount);
            return View(topMovies);
        }

        // GET: MovieRatingController/TopX/{movieCount}
        [Route("MovieRating/TopX/{movieCount:int}/{userId:guid}")]
        public async Task<ActionResult> TopXByUser(byte movieCount, Guid userId)
        {
            if (! await _userService.UserExists(userId, GetCancellationToken()))
            {
                return NotFound("User not found");
            }
            var topMovies = await _movieService.TopMoviesByUser(movieCount, userId, GetCancellationToken());
            await SetViewBagData(movieCount, userId);
            return View(topMovies);
        }

        // POST: MovieRatingController/Rate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Rate(MovieRating movieRating)
        {
            var validationResult = await _ratingService.ValidateMovieRating(movieRating, GetCancellationToken());

            switch (validationResult)
            {
                case MovieRatingSaveValidationResults.OK:
                    if (await _ratingService.SaveRating(movieRating, GetCancellationToken()))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("couldn't save rating");
                    }
                case MovieRatingSaveValidationResults.NullRating:
                case MovieRatingSaveValidationResults.InvalidMovieId:
                case MovieRatingSaveValidationResults.InvalidUserId:
                    return BadRequest(validationResult.ToString());
                case MovieRatingSaveValidationResults.MovieNotfound:
                case MovieRatingSaveValidationResults.UserNotFound:
                    return NotFound(validationResult.ToString());
            }
            return BadRequest("unknown validation failure");
        }

        private async Task SetViewBagData(byte movieCount, Guid? userId = null)
        {
            ViewBag.MovieCount = movieCount;
            if (userId.HasValue)
            {
                var user = await _userService.GetUser(userId.Value, GetCancellationToken());
                if (user != null)
                {
                    ViewBag.UserName = user.UserName;
                }
            }
        }
    }
}
