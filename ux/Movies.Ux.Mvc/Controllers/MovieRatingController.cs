using Microsoft.AspNetCore.Mvc;
using Movies.Business;
using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;

namespace Movies.Ux.Mvc.Controllers
{
    public class MovieRatingController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IRatingService _ratingService;

        public MovieRatingController(IMovieService movieService, IRatingService ratingService)
        {
            _movieService = movieService;
            _ratingService = ratingService;
        }

        // GET: MovieRatingController/TopX/{movieCount}
        [Route("MovieRating/TopX/{movieCount:int}")]
        public async Task<ActionResult> TopX(byte movieCount)
        {
            var topMovies = await _movieService.TopMoviesAsync(movieCount);
            return View(topMovies);
        }

        // POST: MovieRatingController/Rate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Rate(MovieRating movieRating)
        {
            var validationResult = await _ratingService.ValidateMovieRatingAsync(movieRating);

            switch (validationResult)
            {
                case MovieRatingSaveValidationResults.OK:
                    if (await _ratingService.SaveRatingAsync(movieRating))
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

    }
}
