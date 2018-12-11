using Newtonsoft.Json;
using Restonode.Business.Interfaces;
using Restonode.DAL.Configuration;
using Restonode.DAL.Entities;
using System.Threading.Tasks;

namespace Restonode.Business.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        public async Task<object> ApplyRatingAsync(object toInsert)
        {
            return await Task.Run(() =>
            {
                var score = JsonConvert.DeserializeObject<Score>(toInsert.ToString());

                using (var session = SessionNHibernate.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(score);

                    transaction.Commit();

                    return score;
                }
            });
        }
    }
}