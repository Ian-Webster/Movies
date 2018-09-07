using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    /// <summary>
    /// Saves movie ratings
    /// </summary>
    [Produces("application/json")]
    [Route("api/SaveMovieRating")]
    public class SaveMovieRatingController : Controller
    {
        private readonly IRatingService _ratingService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="ratingService"></param>
        public SaveMovieRatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        /// <summary>
        /// saves a movie rating
        /// </summary>
        /// <param name="movieRating"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MovieRating movieRating)
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