using HaroldAdviser.Data;
using HaroldAdviser.ViewModels;
using System.Threading.Tasks;

namespace HaroldAdviser.BL
{
    public interface ICloudInstanceManager
    {
        Task CreateInstanceAsync(IWebhook webhook, RepositorySettings settings);
        Task DropInstanceAsync();
    }
}