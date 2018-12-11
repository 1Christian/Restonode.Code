using System.Threading.Tasks;

namespace Restonode.Common.Interfaces
{
    public interface INotifier
    {
        Task<object> NotifyAsync(byte[] message);
    }
}