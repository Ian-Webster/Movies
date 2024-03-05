using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;
using Movies.Domain.DTO;
using Movies.Ux.Mvc.Controllers;

namespace Movies.Ux.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var users = await _userService.AllUsers(GetCancellationToken());
            return View(users);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var user = await _userService.GetUser(id, GetCancellationToken());
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View(new User { Id = Guid.NewGuid() });
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            var userAdded = await _userService.SaveUser(user, GetCancellationToken());
            if (userAdded)
            {
                return RedirectToAction(nameof(Details), new { id = user.Id });
            }
            else
            {
                return BadRequest("Unable to save user");
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await _userService.GetUser(id, GetCancellationToken());
            return user != null ? View(user) : NotFound();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            var userUpdated = await _userService.SaveUser(user, GetCancellationToken());
            return userUpdated ? RedirectToAction(nameof(Details), new { id = user.Id }) : BadRequest("Unable to save user");
        }
    }
}
