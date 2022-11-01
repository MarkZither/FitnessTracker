using FitnessTracker.Maui.Data;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Maui.Services
{
    public class RouteStorageService : IRouteStorageService
    {
        private readonly ILogger<RouteStorageService> _logger;
        private readonly TrackerContext _context;
        public RouteStorageService(ILogger<RouteStorageService> logger, TrackerContext context)
        {
            _logger = logger;
            _context = context;
        }
        public void SaveRoute(IEnumerable<Location> locations)
        {
            IEnumerable<TrackerLocation> trackerLocations = from x in locations
                                                            select new TrackerLocation(){
                                                                Latitude = x.Latitude,
                                                                Longitude = x.Longitude
                                                            };
            trackerLocations.Count();
            _context.AddRange(trackerLocations);
            try
            {
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Saving Route");
            }
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
