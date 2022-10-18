using FitnessTracker.Maui.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Maui.Services
{
    public class RouteStorageService : IRouteStorageService
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
