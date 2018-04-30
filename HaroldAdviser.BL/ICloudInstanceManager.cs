using System.Threading.Tasks;
using HaroldAdviser.Data;
using HaroldAdviser.ViewModels;

namespace HaroldAdviser.BL
{
    public interface ICloudInstanceManager
    {
        Task CreateInstanceAsync(IWebhook webhook, RepositorySettings settins);
        Task DropInstanceAsync();
    }
}