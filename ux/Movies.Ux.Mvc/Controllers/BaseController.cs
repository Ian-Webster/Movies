using Microsoft.AspNetCore.Mvc;

namespace Movies.Ux.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected CancellationToken GetCancellationToken()
        {
            return HttpContext.RequestAborted;
        }
    }
}
