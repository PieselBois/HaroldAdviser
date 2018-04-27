using HaroldAdviser.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HaroldAdviser.Controllers
{
    public class HomeController : BaseController
    {
        private ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Models.Repository> ShowRepository(string id)
        {
            var Id = Decode(id);
            var repo = await _context.Repositories.FindAsync(Id);
            var model = new Models.Repository
            {
                Url = repo.Url,
                Active = repo.Checked
            };

            return model;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Repo()
        {
            return View(ShowRepository(""));
        }
    }
}