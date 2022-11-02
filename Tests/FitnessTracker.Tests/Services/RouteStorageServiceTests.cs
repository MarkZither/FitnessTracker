using FitnessTracker.Maui.Data;
using FitnessTracker.Maui.Services;
using FitnessTracker.Maui.ViewModels;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Serilog;

using SkiaSharp;

namespace FitnessTracker.Tests.Services
{
    public class RouteStorageServiceTests : IDisposable
    {
        private readonly IRouteStorageService subRouteStorageService;
        private readonly ILogger<RouteStorageServiceTests> _logger;

        //private readonly TrackerContext _context;
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<TrackerContext> _contextOptions;
        TrackerContext CreateContext() => new TrackerContext(_contextOptions);

        public RouteStorageServiceTests()
        {
            this._logger = Substitute.For<ILogger<RouteStorageServiceTests>>();
            var serviceLogger = Substitute.For<ILogger<RouteStorageService>>();
            
            // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            //_connection = new SqliteConnection("Filename=:memory:");
            _connection = new SqliteConnection($"Filename={Guid.NewGuid()}.db");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<TrackerContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema and seed some data
            using var context = new TrackerContext(_contextOptions);

            if (context.Database.EnsureCreated())
            {
            }
            this.subRouteStorageService = new RouteStorageService(serviceLogger, context);
        }

        [Fact]
        public void SaveARoute_EmptyDatabase_RouteSavedAndReturnedAsFirstRecord()
        {
            // Arrange
            var context = CreateContext();

            // Act
            List<TrackerLocation> locs = new List<TrackerLocation>();
            TrackerLocation loc = new TrackerLocation() {Latitude = 21, Longitude = 50 };
            locs.Add(loc);
            Route route = new Route() { Locations = locs };
            context.Routes.Add(route);
            context.SaveChanges();

            // Assert
            Assert.Collection<TrackerLocation>(context.Routes.First().Locations.ToList(), 
                item => Assert.Equal(loc, item));
            Assert.Equal(context.Routes.First().Locations.First(), loc);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
