﻿using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    /// <summary>
    /// movie controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Movie")]
    public class MovieController : BaseController
    {

        private readonly IMovieService _movieService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="movieService"></param>
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Saves a movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Movie))]
        public async Task<IActionResult> Post([FromBody]Movie movie)
        {
            if (await _movieService.SaveMovie(movie, GetCancellationToken()))
            {
                return Ok();
            }
            else
            {
                return BadRequest("unable to save movie");
            }
        }

    }
}