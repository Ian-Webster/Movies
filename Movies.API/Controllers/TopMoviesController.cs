using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;

namespace Movies.API.Controllers
{
    /// <summary>
    /// Gets top movies by user rating
    /// </summary>
    [Produces("application/json")]
    [Route("api/TopMovies")]
    public class TopMoviesController : Controller
    {
        private static byte movieCount = 5;

        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="movieService"></param>
        /// <param name="userService"></param>
        public TopMoviesController(IMovieService movieService, IUserService userService)
        {
            _movieService = movieService;
            _userService = userService;
        }

        /// <summary>
        /// Get's top 5 movies across all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var movies = _movieService.TopMovies(movieCount);

            if (movies == null || !movies.Any())
            {
                return NotFound();
            }

            return Json(movies);
        }

        /// <summary>
        /// Gets top 5 movies for a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("userId")]
        public IActionResult Get(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("userId is zero or negative");
            }

            if (!_userService.UserExists(userId))
            {
                return BadRequest("userId is invalid");
            }

            var movies = _movieService.TopMoviesByUser(movieCount, userId);

            if (movies == null || !movies.Any())
            {
                return NotFound();
            }

            return Json(movies);
        }

    }
}