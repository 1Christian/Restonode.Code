using System.Threading.Tasks;

namespace Restonode.API.Interfaces
{
    public interface IPublisher
    {
        Task<bool> PublishAsync(object message);
    }
}