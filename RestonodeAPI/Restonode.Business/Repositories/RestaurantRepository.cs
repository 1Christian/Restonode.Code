using Restonode.DAL.Configuration;
using Restonode.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Restonode.Business.Interfaces;
using Newtonsoft.Json;

namespace Restonode.Business.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        public async Task<object> CreateRestaurantAsync(object toInsert)
        {
            return await Task.Run(() =>
            {
                var restaurant = JsonConvert.DeserializeObject<Restaurant>(toInsert.ToString());

                using (var _session = SessionNHibernate.OpenSession())
                using (var transaction = _session.BeginTransaction())
                {
                    _session.SaveOrUpdate(restaurant);

                    transaction.Commit();
                };

                return restaurant;
            });
        }

        public async Task<object> FilterByRaitingAsync(double rating)
        {
            return await Task.Run(() =>
            {
                using (var _session = SessionNHibernate.OpenSession())
                {
                    return _session.QueryOver<Restaurant>()
                        .Where(x => x.Scores.Select(s => s.RatingScore).Average() == rating).SingleOrDefaultAsync();                    
                }
            });
        }

        public async Task<object> GetRestaurantByIdAsync(int id)
        {
            return await Task.Run(() =>
            {
                using (var _session = SessionNHibernate.OpenSession())
                {
                    return _session.QueryOver<Restaurant>().Where(x => x.RestaurantID == id).SingleOrDefault();
                }
            });
        }

        public async Task<IEnumerable<object>> GetRestaurantsAsync()
        {
            return await Task.Run(() =>
            {
                using (var _session = SessionNHibernate.OpenSession())
                {
                    return _session.CreateCriteria<Restaurant>().List<Restaurant>();
                }
            });
        }
    }
}