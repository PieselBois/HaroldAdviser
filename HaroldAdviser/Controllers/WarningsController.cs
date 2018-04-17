using HaroldAdviser.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HaroldAdviser.Controllers
{
    [Route("/api/warnings")]
    public class WarningsController : Controller
    {
        private readonly ApplicationContext _context;

        public WarningsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add/{Key}")]
        public IActionResult Add(string key, [FromBody] IList<WarningModel> model)
        {
            var log = new Warning
            {
                Kind = model.First().Kind,
                File = model.First().File,
                Lines = model.First().Lines,
                Message = model.First().Message
            };
            var repository = _context.Repositories.Include(r => r.Warnings).First(r => r.Token == key);
            repository.Warnings.Add(log);
            _context.SaveChanges();
            return Ok();
        }
    }
}
