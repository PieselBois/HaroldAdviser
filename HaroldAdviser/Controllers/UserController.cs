using HaroldAdviser.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Repository = HaroldAdviser.Data.Repository;

namespace HaroldAdviser.Controllers
{
    public class UserController : BaseController
    {
        private readonly ApplicationContext _context;


        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize, Route("/Account")]
        public IActionResult Account()
        {
            return View();
        }

        [HttpGet, Authorize, Route("/api/User/Repository/sync")]
        public async Task<IActionResult> SyncRepositories()
        {
            var user = GetUser();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repositories = await GetRepositories();

            foreach (var repository in repositories)
            {
                if (_context.Repositories.Any(r => r.Url == repository.HtmlUrl))
                {
                    continue;
                }

                _context.Repositories.Add(new Repository
                {
                    UserId = user.Id,
                    Url = repository.HtmlUrl,
                    ApiKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", "")
                });
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet, Authorize, Route("/api/User/Repository")]
        public IActionResult ShowRepositories()
        {
            var user = GetUser();

            var repositories = _context.Repositories.Where(r => r.UserId == user.Id);

            return Json(repositories.Select(r => new ViewModels.Repository
            {
                Url = r.Url,
                Active = r.Checked,
                Id = Encode(r.Id)
            }));
        }

        [HttpGet, Authorize, Route("/User/Repository/{repositoryId}")]
        public async Task<IActionResult> RepositoryInfo([FromRoute] string repositoryId)
        {
            var id = Decode(repositoryId);
            var repo = await _context.Repositories.FindAsync(id);
            if (repo == null)
            {
                return NotFound();
            }

            var model = new ViewModels.Repository
            {
                Url = repo.Url,
                Active = repo.Checked
            };
            return View(model);
        }

        /// <summary>
        /// Set {Checked} on repository with {repositoryId} to opposite
        /// </summary>
        /// <param name="repositoryId"> base64 encoded repository guid</param>
        /// <returns>Http status</returns>
        [HttpPost, Authorize, Route("/User/Repository/Check/{repositoryId}")]
        public async Task<IActionResult> CheckRepository([FromRoute] string repositoryId)
        {
            var id = Decode(repositoryId);
            var repo = await _context.Repositories.FindAsync(id);
            if (repo == null)
            {
                return NotFound();
            }

            repo.Checked = !repo.Checked;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}