using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;

namespace Movies.Ux.Mvc.Controllers
{

    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: MovieController
        public async Task<ActionResult> Index()
        {
            var movies = await _movieService.GetMovies(GetCancellationToken());
            return View(movies);
        }

        // GET: MovieController/Details/5
        public async Task<ActionResult<Domain.DTO.Movie>> Details(Guid id)
        {
            try
            {
                var movie = await _movieService.GetMovie(id, GetCancellationToken());
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
