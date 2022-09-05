using SharpGPX;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Maui.Services
{
    public interface IGeolocationService
    {
        public Task<Location> GetLocationAsync(GeolocationAccuracy Accuracy, CancellationTokenSource cts);
        public Task<Location> GetLastKnownLocationAsync();
        string FormatLocation(Location location, Exception ex = null);
    }
    public class GeolocationService : IGeolocationService
    {
        string notAvailable = "not available";
        public async Task<Location> GetLocationAsync(GeolocationAccuracy Accuracy, CancellationTokenSource cts)
        {
            var request = new GeolocationRequest(Accuracy);
            var location = await Geolocation.GetLocationAsync(request, cts.Token);
            return location;
        }
        public async Task<Location> GetLastKnownLocationAsync()
        {
            return await Geolocation.GetLastKnownLocationAsync();
        }
        public string FormatLocation(Location location, Exception ex = null)
        {
            if (location == null)
            {
                return $"Unable to detect location. Exception: {ex?.Message ?? string.Empty}";
            }

            return
                $"Latitude: {location.Latitude}\n" +
                $"Longitude: {location.Longitude}\n" +
                $"HorizontalAccuracy: {location.Accuracy}\n" +
                $"Altitude: {(location.Altitude.HasValue ? location.Altitude.Value.ToString() : notAvailable)}\n" +
                $"AltitudeRefSys: {location.AltitudeReferenceSystem.ToString()}\n" +
                $"VerticalAccuracy: {(location.VerticalAccuracy.HasValue ? location.VerticalAccuracy.Value.ToString() : notAvailable)}\n" +
                $"Heading: {(location.Course.HasValue ? location.Course.Value.ToString() : notAvailable)}\n" +
                $"Speed: {(location.Speed.HasValue ? location.Speed.Value.ToString() : notAvailable)}\n" +
                $"Date (UTC): {location.Timestamp:d}\n" +
                $"Time (UTC): {location.Timestamp:T}\n" +
                $"Mocking Provider: {location.IsFromMockProvider}";
        }
    }

    public class MockGeolocationService : IGeolocationService
    {
        string notAvailable = "not available";
        int trkptIndex = 0;
        private readonly GpxClass gpxClass;

        public MockGeolocationService()
        {
            using var stream = Task.Run(() => FileSystem.OpenAppPackageFileAsync("2022-05-02_20-01-31_-_walking.gpx")).GetAwaiter().GetResult();
            gpxClass = GpxClass.FromStream(stream);
        }
        public string FormatLocation(Location location, Exception ex = null)
        {
            return location is null ? ex.ToString() :
                $"Latitude: {location.Latitude}\n" +
                $"Longitude: {location.Longitude}\n" +
                $"HorizontalAccuracy: {location.Accuracy}\n" +
                $"Altitude: {(location.Altitude.HasValue ? location.Altitude.Value.ToString() : notAvailable)}\n" +
                $"AltitudeRefSys: {location.AltitudeReferenceSystem.ToString()}\n" +
                $"VerticalAccuracy: {(location.VerticalAccuracy.HasValue ? location.VerticalAccuracy.Value.ToString() : notAvailable)}\n" +
                $"Heading: {(location.Course.HasValue ? location.Course.Value.ToString() : notAvailable)}\n" +
                $"Speed: {(location.Speed.HasValue ? location.Speed.Value.ToString() : notAvailable)}\n" +
                $"Date (UTC): {location.Timestamp:d}\n" +
                $"Time (UTC): {location.Timestamp:T}\n" +
                $"Mocking Provider: {location.IsFromMockProvider}";
        }

        public Task<Location> GetLastKnownLocationAsync()
        {
            return Task.FromResult(new Location(49.621953, 6.092523));
        }

        public async Task<Location> GetLocationAsync(GeolocationAccuracy Accuracy, CancellationTokenSource cts)
        {
            await Task.Delay(750);
            if(trkptIndex >= gpxClass.Tracks[0].trkseg[0].trkpt.Count() - 1)
            { trkptIndex = 0; }
            var trkpt = gpxClass.Tracks[0].trkseg[0].trkpt[trkptIndex];
            trkptIndex++;
            return new Location((double)trkpt.lat, (double)trkpt.lon, trkpt.time) { Altitude = (double)trkpt.ele };
        }
    }
}
