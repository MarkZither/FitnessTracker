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
            IEnumerable<TrackerLocation> trackerLocations = from x in locations
                                                            select new TrackerLocation(){
                                                                Latitude = x.Latitude,
                                                                Longitude = x.Longitude
                                                            };
            trackerLocations.Count();
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
