﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Domain.Enums;
using Movies.Ux.Mvc.Controllers;

namespace Movies.Ux.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<ActionResult<Movie>> Details(Guid id)
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

        // GET: MovieController/Create
        public ActionResult Create()
        {
            SetGenreSelectList();
            return View(new Movie{ Id = Guid.NewGuid()});
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            var movieAdded = await _movieService.SaveMovie(movie, GetCancellationToken());
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
            var movie = await _movieService.GetMovie(id, GetCancellationToken());
            SetGenreSelectList();
            return movie != null ? View(movie) : NotFound();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Movie movie)
        {
            var movieUpdated = await _movieService.SaveMovie(movie, GetCancellationToken());

            return movieUpdated ? RedirectToAction(nameof(Details), new { id = movie.Id }) : BadRequest("unable to save movie");
        }

        private void SetGenreSelectList()
        {
            ViewBag.GenreSelectList = new SelectList(Enum.GetValues(typeof(Genres)));
        }

    }
}
