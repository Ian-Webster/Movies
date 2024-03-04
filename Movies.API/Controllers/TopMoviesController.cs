using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;
using Movies.Domain.DTO;

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
        [ProducesResponseType(200, Type = typeof(List<Movie>))]
        public async Task<IActionResult> Get()
        {
            var movies = await _movieService.TopMoviesAsync(movieCount);

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
        [ProducesResponseType(200, Type = typeof(List<Movie>))]
        public async Task<IActionResult> Get(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("userId is empty");
            }

            if (! await _userService.UserExistsAsync(userId))
            {
                return BadRequest("userId is invalid");
            }

            var movies = await _movieService.TopMoviesByUserAsync(movieCount, userId);

            if (movies == null || !movies.Any())
            {
                return NotFound();
            }

            return Json(movies);
        }

    }
}