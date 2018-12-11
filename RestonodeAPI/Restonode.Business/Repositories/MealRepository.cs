using System.Threading.Tasks;
using Newtonsoft.Json;
using Restonode.Business.Interfaces;
using Restonode.DAL.Configuration;
using Restonode.DAL.Entities;
using System.Linq;

namespace Restonode.Business.Repositories
{
    public class MealRepository : IMealRepository
    {
        public async Task<object> AddMealAsync(object toInsert)
        {
            return await Task.Run(async () =>
            {
                var meal = JsonConvert.DeserializeObject<Meal>(toInsert.ToString());

                using (var session = SessionNHibernate.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    await session.SaveOrUpdateAsync(meal);

                    await transaction.CommitAsync();
                    
                };

                return meal;
            });
        }
    }
}