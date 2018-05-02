using HaroldAdviser.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet, Authorize, Route("/User")]
        public IActionResult Account()
        {
            return View();
        }

        [HttpGet, Authorize, Route("/User/Repository/Check")]
        public IActionResult AllRepositories()
        {
            return View();
        }

        [HttpGet, Authorize, Route("/Api/User/Repository/Sync")]
        public async Task<IActionResult> SyncRepositories()
        {
            var user = GetUser();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var remoteRepositories = await GetRepositories();

            var dbrepos = _context.Repositories.Where(r => r.UserId == user.Id);

            var dburls = dbrepos.Select(r => r.Url).ToHashSet();

            var remoteUrls = remoteRepositories.Select(r => r.HtmlUrl).ToHashSet();

            var reposToDelete = dbrepos.Where(r => !remoteUrls.Contains(r.Url));

            await _context.Repositories.AddRangeAsync(remoteRepositories.Where(r => !dburls.Contains(r.HtmlUrl)).Select(
                r => new Repository
                {
                    UserId = user.Id,
                    Url = r.HtmlUrl,
                    Name = r.FullName
                }));

            _context.Repositories.RemoveRange(reposToDelete);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet, Authorize, Route("/Api/User/Repository")]
        public IActionResult ShowRepositories()
        {
            var user = GetUser();

            var repositories = _context.Repositories.Where(r => r.UserId == user.Id);

            return Json(repositories.Select(r => new ViewModels.Repository
            {
                Url = r.Url,
                Active = r.Checked,
                Id = Encode(r.Id),
                Name = r.Name
            }).OrderBy(r => r.Url));
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
                Active = repo.Checked,
                Name = repo.Name
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

        [HttpGet, Authorize, Route("/Api/User/Repository/Warnings")]
        public IActionResult GetWarnings()
        {
            var warnings = new List<Warning>
            {
                new Warning
                {
                    Kind = "style",
                    File = "/home/fexolm/git/SLama/tests/block-test.c",
                    Lines = "15, 16",
                    Message = "Variable 'block' is reassigned a value before the old one has been used."
                },
                new Warning
                {
                    Kind = "style",
                    File = "/home/fexolm/git/SLama/tests/block-test.c",
                    Lines = "30, 33",
                    Message = "Variable 'block' is reassigned a value before the old one has been used."
                },
                new Warning
                {
                    Kind = "style",
                    File = "/home/fexolm/git/SLama/tests/block-test.c",
                    Lines = "31, 35",
                    Message = "Variable 'block2' is reassigned a value before the old one has been used."
                },
                new Warning
                {
                    Kind = "style",
                    File = "/home/fexolm/git/SLama/tests/block-test.c",
                    Lines = "55, 58",
                    Message = "Variable 'block' is reassigned a value before the old one has been used."
                },
                new Warning
                {
                    Kind = "style",
                    File = "/home/fexolm/git/SLama/tests/block-test.c",
                    Lines = "79, 82",
                    Message = "Variable 'block' is reassigned a value before the old one has been used."
                },
                new Warning
                {
                    Kind = "style",
                    File = "/home/fexolm/git/SLama/tests/block-test.c",
                    Lines = "80, 83",
                    Message = "Variable 'block2' is reassigned a value before the old one has been used."
                }
            };

            return Json(warnings);
        }
    }
}