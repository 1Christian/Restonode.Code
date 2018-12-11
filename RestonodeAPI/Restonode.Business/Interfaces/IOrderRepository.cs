using Restonode.Common.Interfaces;
using System.Threading.Tasks;

namespace Restonode.Business.Interfaces
{
    public interface IOrderRepository
    {
        Task<object> CreateOrderAsync(object order);

        Task<IMessage> GetOrderMessageAsync(object order);
    }
}