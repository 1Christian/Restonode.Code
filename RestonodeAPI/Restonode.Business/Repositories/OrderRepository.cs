using Newtonsoft.Json;
using Restonode.Business.Interfaces;
using Restonode.Common.Interfaces;
using Restonode.DAL.Configuration;
using Restonode.DAL.Entities;
using Restonode.Repositories;
using System.Threading.Tasks;

namespace Restonode.Business.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMessage _message;
        //rivate readonly ILocationManager _locationManager;

        public OrderRepository(IMessage message)
        {
            _message = message;
            //_locationManager = locationManager;
        }

        public async Task<object> CreateOrderAsync(object toInsert)
        {
            return await Task.Run(() =>
            {
                var order = JsonConvert.DeserializeObject<CustomerOrder>(toInsert.ToString());

                //RootObject customerLocation = await _locationManager.GetLocationAsync(order.Address);

                //RootObject restaurantLocation = await _locationManager.GetLocationAsync(order.Restaurant.Address);

                //Gets latitude and Longitude then set the order object with coordinates
                //_locationManager.CalculateETA(customerLocation, restaurantLocation);

                using (var session = SessionNHibernate.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(order);

                    transaction.Commit();
                };

                return order;
            });
        }

        public async Task<IMessage> GetOrderMessageAsync(object order)
        {
            return await _message.SetMessageAsync((CustomerOrder)order);
        }
    }
}