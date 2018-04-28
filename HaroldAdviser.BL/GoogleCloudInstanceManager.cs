using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Compute.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using DataCloud = Google.Apis.Compute.v1.Data;


namespace HaroldAdviser.BL
{
    public class GoogleCloudInstanceManager : ICloudInstanceManager
    {
        private readonly IConfiguration _configuration;

        public GoogleCloudInstanceManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<GoogleCredential> GetCredentialAsync()
        {
            var credential = await GoogleCredential.GetApplicationDefaultAsync();
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(_configuration["vm_conf:credential_url"]);
            }

            return credential;
        }

        public async Task CreateInstanceAsync()
        {
            var computeService = new ComputeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await GetCredentialAsync(),
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

        public async Task DropInstanceAsync()
        {
            var computeService = new ComputeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await GetCredentialAsync(),
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
    }
}