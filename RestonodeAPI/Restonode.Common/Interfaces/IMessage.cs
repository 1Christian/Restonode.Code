using Restonode.DAL.Entities;
using System.Threading.Tasks;

namespace Restonode.Common.Interfaces
{
    public interface IMessage
    {
        string Body { set; get; }

        Task<IMessage> SetMessageAsync(CustomerOrder order);
    }
}