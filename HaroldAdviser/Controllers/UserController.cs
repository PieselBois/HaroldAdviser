using Google.Apis.Auth.OAuth2;
using Google.Apis.Compute.v1;
using Google.Apis.Services;
using HaroldAdviser.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DataCloud = Google.Apis.Compute.v1.Data;
using Repository = HaroldAdviser.Data.Repository;

namespace HaroldAdviser.Controllers
{
    public class UserController : BaseController
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public UserController(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

            return Json(repositories.Select(r => new Models.Repository
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

            var model = new Models.Repository
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

        #region Move to HaroldAdviser.BL

        private async Task<GoogleCredential> GetCredential()
        {
            var credential = await GoogleCredential.GetApplicationDefaultAsync();
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(_configuration["vm_conf:credential_url"]);
            }

            return credential;
        }

        public async Task CreateInstance()
        {
            var computeService = new ComputeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await GetCredential(),
                ApplicationName = _configuration["vm_conf:computeservice_name"],
            });

            var project = Environment.GetEnvironmentVariable("GOOGLE_PROJECT");

            var zone = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ZONE");

            var requestBody = new DataCloud.Instance
            {
                Name = _configuration["vm_conf:instance_name"],
                MachineType = string.Format(_configuration["vm_conf:machine_type"], project, zone),
                NetworkInterfaces = new List<DataCloud.NetworkInterface>
                {
                    new DataCloud.NetworkInterface
                    {
                        Network = string.Format(_configuration["vm_conf:network"], project),
                        AccessConfigs = new List<DataCloud.AccessConfig>
                        {
                            new DataCloud.AccessConfig
                            {
                                Name = _configuration["vm_conf:access_name"],
                                Type = _configuration["vm_conf:access_type"]
                            }
                        }
                    }
                },
                Disks = new List<DataCloud.AttachedDisk>
                {
                    new DataCloud.AttachedDisk
                    {
                        DeviceName = _configuration["vm_conf:disk_name"],
                        Type = _configuration["vm_conf:disk_type"],
                        Boot = true,
                        AutoDelete = true,
                        InitializeParams = new DataCloud.AttachedDiskInitializeParams
                        {
                            SourceImage = _configuration["vm_conf:disk_image"]
                        }
                    }
                }
            };

            var request = computeService.Instances.Insert(requestBody, project, zone);

            var response = await request.ExecuteAsync();

            Console.WriteLine(JsonConvert.SerializeObject(response));
        }

        public async Task DropInstance()
        {
            var computeService = new ComputeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await GetCredential(),
                ApplicationName = _configuration["vm_conf:computeservice_name"],
            });

            var project = Environment.GetEnvironmentVariable("GOOGLE_PROJECT");

            var zone = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ZONE");

            var instance = _configuration["vm_conf:instance_name"];

            var requestStop = computeService.Instances.Stop(project, zone, instance);

            var responseStop = await requestStop.ExecuteAsync();

            Console.WriteLine(JsonConvert.SerializeObject(responseStop));

            var requestDelete = computeService.Instances.Delete(project, zone, instance);

            var responseDelete = await requestDelete.ExecuteAsync();

            Console.WriteLine(JsonConvert.SerializeObject(responseDelete));
        }

        #endregion
    }
}