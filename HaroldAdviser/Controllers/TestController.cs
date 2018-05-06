using HaroldAdviser.BL;
using HaroldAdviser.Data;
using HaroldAdviser.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HaroldAdviser.Controllers
{
    public class TestController : Controller
    {
        private readonly ICloudInstanceManager _instanceManager;
        private readonly ApplicationContext _context;

        public TestController(ICloudInstanceManager instanceManager, ApplicationContext context)
        {
            _instanceManager = instanceManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateInstance()
        {
            await _instanceManager.CreateInstanceAsync(null, null);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> DropInstance()
        {
            await _instanceManager.DropInstanceAsync();
            return Ok();
        }

        [HttpGet, Route("/Api/Test/Logs")]
        public IActionResult GetLogs()
        {
            var logs = _context.Logs;

            return Json(logs.Select(r => new Log
            {
                Module = r.Module,
                Type = r.Type,
                Value = r.Value,
                Id = r.Id
            }));
        }

        [HttpGet, Route("/Api/Pipeline/Get")]
        public async Task<IActionResult> GetPipelineAsync()
        {
            var pipeline = await _context.Pipelines.Include(r => r.Repository)
                .OrderByDescending(r => r.Date).Where(r => r.Status == 0)
                .LastOrDefaultAsync();

            if (pipeline == null)
            {
                return NotFound();
            }

            pipeline.Status = PipelineStatus.Started;

            await _context.SaveChangesAsync();

            return Json(new ViewModels.Pipeline
            {
                Id = pipeline.Id,
                Url = pipeline.CloneUrl,
                Date = pipeline.Date,
                Status = pipeline.Status,
                CommitId = pipeline.CommitId
            });
        }
    }
}