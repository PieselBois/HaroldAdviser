using Microsoft.AspNetCore.Mvc;

namespace HaroldAdviser.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}