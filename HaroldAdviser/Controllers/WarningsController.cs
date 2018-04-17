using HaroldAdviser.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HaroldAdviser.Controllers
{
    [Route("/api")]
    public class WarningsController : Controller
    {
        private readonly ApplicationContext _context;

        public WarningsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("WarningsHandler/{Key}")]
        public IActionResult WarningsHandler(string key, [FromBody] ErrorModel model)
        {
            var log = new LogModel
            {
                Kind = model.Info.First().Kind,
                File = model.Info.First().File,
                Lines = model.Info.First().Lines,
                Message = model.Info.First().Message
            };
            var repository = _context.Repositories.Include(r => r.Logs).First(r => r.Token == key);
            repository.Logs.Add(log);
            _context.SaveChanges();
            return Ok();
        }
    }
}
