using HaroldAdviser.BL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HaroldAdviser.Controllers
{
    public class TestController : Controller
    {
        private readonly ICloudInstanceManager _instanceManager;

        public TestController(ICloudInstanceManager instanceManager)
        {
            _instanceManager = instanceManager;
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
    }
}