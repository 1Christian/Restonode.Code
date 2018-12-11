using Restonode.Repositories;
using System.Threading.Tasks;

namespace Restonode.Business.Interfaces
{
    public interface ILocationManager
    {
        Task<RootObject> GetLocationAsync(string address);

        void CalculateETA(RootObject origin, RootObject destination);
    }
}