using Microsoft.AspNetCore.Mvc;

namespace HaroldAdviser.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(GetUser());
            }
            return View();
        }
    }
}