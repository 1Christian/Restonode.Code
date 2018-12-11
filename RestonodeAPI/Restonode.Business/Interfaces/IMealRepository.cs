using System.Threading.Tasks;

namespace Restonode.Business.Interfaces
{
    public interface IMealRepository
    {
        Task<object> AddMealAsync(object meal);
    }
}