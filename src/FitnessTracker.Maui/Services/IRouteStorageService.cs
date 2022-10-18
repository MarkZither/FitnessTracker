using FitnessTracker.Maui.Data;

namespace FitnessTracker.Maui.Services
{
    public interface IRouteStorageService
    {
        void SaveRoute(IEnumerable<Location> locations);
        IEnumerable<Route> GetRoutes();
        Route GetRoute(int routeId);
    }
}
