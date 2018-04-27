using HaroldAdviser.Data;
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

        [HttpPost, Route("add/{Key}")]
        public IActionResult Add(string key, [FromBody] IList<WarningModel> model)
        {
            foreach (var element in model)
            {
                var log = new Warning
                {
                    Kind = element.Kind,
                    File = element.File,
                    Lines = element.Lines,
                    Message = element.Message
                };
                var repository = _context.Repositories.Include(r => r.Warnings).First(r => r.ApiKey == key);
                repository.Warnings.Add(log);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}