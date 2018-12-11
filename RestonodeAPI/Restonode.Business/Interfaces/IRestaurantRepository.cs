using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restonode.Business.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<object> FilterByRaitingAsync(double rating);

        Task<object> CreateRestaurantAsync(object restaurant);

        Task<IEnumerable<object>> GetRestaurantsAsync();

        Task<object> GetRestaurantByIdAsync(int id);
    }
}