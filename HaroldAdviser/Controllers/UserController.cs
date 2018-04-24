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
using DataCloud = Google.Apis.Compute.v1.Data;
using Repository = HaroldAdviser.Data.Repository;

namespace HaroldAdviser.Controllers
{
    public class UserController : BaseController
    {
        private ApplicationContext _context;
        private IConfiguration _configuration;

        public UserController(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Page()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(GetUser());
            }
            return View();
        }

        private async Task<GoogleCredential> GetCredential()
        {
            var credential = await GoogleCredential.GetApplicationDefaultAsync();
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped("https://www.googleapis.com/auth/cloud-platform");
            }
            return credential;
        }

        protected async Task CreateInstance()
        {
            var computeService = new ComputeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await GetCredential(),
                ApplicationName = "Google-ComputeSample/0.1",
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

        protected async Task DropInstance()
        {
            var computeService = new ComputeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await GetCredential(),
                ApplicationName = "Google-ComputeSample/0.1",
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

        [HttpGet]
        [Authorize]
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
                    Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", "")
                });
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}