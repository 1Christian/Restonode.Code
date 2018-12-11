using System.Threading.Tasks;

namespace Restonode.Business.Interfaces
{
    public interface IScoreRepository
    {
        Task<object> ApplyRatingAsync(object rating);
    }
}