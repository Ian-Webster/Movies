using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;

namespace Movies.Ux.Mvc.Controllers
{
    public class MovieRatingController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieRatingController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: MovieRatingController
        public async Task<ActionResult> Index(byte movieCount)
        {
            var topMovies = await _movieService.TopMoviesAsync(movieCount);
            return View(topMovies);
        }

        // GET: MovieRatingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieRatingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieRatingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieRatingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieRatingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieRatingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieRatingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
