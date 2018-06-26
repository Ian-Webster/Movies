using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Movies.API.Controllers
{
    /// <summary>
    /// Movie search
    /// </summary>
    [Produces("application/json")]
    [Route("api/SearchMovies")]
    public class SearchMoviesController : Controller
    {
        private readonly IMovieService _movieService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="movieService"></param>
        public SearchMoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Searches movies by given criteria
        /// Note: at least one field of the criteria object must be set
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Movie>))]
        public IActionResult Get(MovieSearchCriteria criteria)
        {
            var validationResult = _movieService.ValidateSearchCriteria(criteria);

            switch (validationResult)
            {
                case MovieSearchValidationResults.InvalidCriteria:
                case MovieSearchValidationResults.NoCriteria:
                    return BadRequest(validationResult.ToString());
                
            }

            var movies = _movieService.SearchMovies(criteria);

            if (movies == null || !movies.Any())
            {
                return NotFound();
            }

            return Json(movies);
        }

    }
}