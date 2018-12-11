using System;
using System.Threading.Tasks;
using Restonode.Common.Interfaces;
using Restonode.DAL.Entities;

namespace Restonode.Common
{
    [Serializable]
    public class BaseMessage : IMessage
    {
        public string Body { get ; set; }

        public Task<IMessage> SetMessageAsync(CustomerOrder order)
        {
            return Task.Run(() =>
            {
                var type = order.GetType();

                var properties = type.GetProperties();

                foreach (var propiterator in properties)
                {
                    Body += $"{propiterator.Name} : {propiterator.GetValue(order)}\r\n";
                }

                return (IMessage)this;
            });            
        }
    }
}