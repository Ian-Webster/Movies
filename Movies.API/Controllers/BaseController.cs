using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        protected CancellationToken GetCancellationToken()
        {
            return HttpContext.RequestAborted;
        }

    }
}
