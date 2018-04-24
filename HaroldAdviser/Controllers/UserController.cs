using Google.Apis.Auth.OAuth2;
using Google.Apis.Compute.v1;
using Google.Apis.Services;
using HaroldAdviser.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Page()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(GetUser());
            }
            return View();
        }

        public async Task<GoogleCredential> GetCredential()
        {
            var credential = await GoogleCredential.GetApplicationDefaultAsync();
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped("https://www.googleapis.com/auth/cloud-platform");
            }
            return credential;
        }

        protected async void CreateInstance()
        {
            var computeService = new ComputeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await GetCredential(),
                ApplicationName = "Google-ComputeSample/0.1",
            });

            var project = "haroldci-195817";

            var zone = "europe-west3-b";

            var requestBody = new DataCloud.Instance
            {
                Name = "foo",
                MachineType = "https://www.googleapis.com/compute/v1/projects/" + project + "/zones/" + zone +
                              "/machineTypes/f1-micro",
                NetworkInterfaces = new List<DataCloud.NetworkInterface>
                {
                    new DataCloud.NetworkInterface
                    {
                        Network = "https://www.googleapis.com/compute/v1/projects/" + project +
                                  "/global/networks/default",
                        AccessConfigs = new List<DataCloud.AccessConfig>
                        {
                            new DataCloud.AccessConfig
                            {
                                Name = "External NAT",
                                Type = "ONE_TO_ONE_NAT"
                            }
                        }
                    }
                },
                Disks = new List<DataCloud.AttachedDisk>
                {
                    new DataCloud.AttachedDisk
                    {
                        DeviceName = "boot",
                        Type = "PERSISTENT",
                        Boot = true,
                        AutoDelete = true,
                        InitializeParams = new DataCloud.AttachedDiskInitializeParams
                        {
                            SourceImage =
                                "https://www.googleapis.com/compute/v1/projects/debian-cloud/global/images/family/debian-9"
                        }
                    }
                }
            };

            var request = computeService.Instances.Insert(requestBody, project, zone);

            var response = await request.ExecuteAsync();

            Console.WriteLine(JsonConvert.SerializeObject(response));
        }

        protected async void DropInstance()
        {
            var computeService = new ComputeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await GetCredential(),
                ApplicationName = "Google-ComputeSample/0.1",
            });

            var project = "haroldci-195817";

            var zone = "europe-west3-b";

            var instance = "foo";

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