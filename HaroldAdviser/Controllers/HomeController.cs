using HaroldAdviser.Data;
using Microsoft.AspNetCore.Mvc;

namespace HaroldAdviser.Controllers
{
    public class HomeController : BaseController
    {
        private ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}