using System.Threading.Tasks;

namespace HaroldAdviser.BL
{
    public interface ICloudInstanceManager
    {
        Task CreateInstanceAsync();
        Task DropInstanceAsync();
    }
}