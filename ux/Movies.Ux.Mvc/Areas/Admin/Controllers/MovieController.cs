using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums;

namespace Movies.Ux.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<ActionResult<Movie>> Details(Guid id)
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

        // GET: MovieController/Create
        public ActionResult Create()
        {
            SetGenreSelectList();
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            var movieAdded = await _movieService.SaveMovieAsync(movie);
            if (movieAdded)
            {
                return RedirectToAction(nameof(Details), new { id = movie.Id });
            }
            else
            {
                return BadRequest("unable to save movie");
            }
        }

        // GET: MovieController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            SetGenreSelectList();
            return movie != null ? View(movie) : NotFound();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Movie movie)
        {
            var movieUpdated = await _movieService.SaveMovieAsync(movie);

            return movieUpdated ? RedirectToAction(nameof(Details), new { id = movie.Id }) : BadRequest("unable to save movie");
        }

        private void SetGenreSelectList()
        {
            ViewBag.GenreSelectList = new SelectList(Enum.GetValues(typeof(Genres)));
        }

    }
}
