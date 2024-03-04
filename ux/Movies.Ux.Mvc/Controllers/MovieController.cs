using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Business.Interfaces;
using Movies.Domain.Enums;

namespace Movies.Ux.Mvc.Controllers
{

    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: MovieController
        public async Task<ActionResult> Index()
        {
            var movies = await _movieService.GetMoviesAsync();
            return View(movies);
        }

        // GET: MovieController/Details/5
        public async Task<ActionResult<Domain.DTO.Movie>> Details(Guid id)
        {
            try
            {
                var movie = await _movieService.GetMovieAsync(id);
                return View(movie);
            }
            catch (Exception ex)
            {
                // Log the exception here
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
