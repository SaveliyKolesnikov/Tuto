using Microsoft.AspNetCore.Mvc;
using Tuto.API.Authorization;

namespace Tuto.API.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("home/index")]
        [AuthFilter(AuthRole.Admin)]
        public IActionResult Index()
        {
            return Content("123");
        }
    }
}
