using FitnessTracker.Maui.Data;

namespace FitnessTracker.Maui.Services
{
    public class MockRouteStorageService : IRouteStorageService
    {
        public void SaveRoute(IEnumerable<Location> locations)
        {

        }

        public IEnumerable<Route> GetRoutes()
        {
            return default;
        }

        public Route GetRoute(int routeId)
        {
            return default;
        }
    }
}
