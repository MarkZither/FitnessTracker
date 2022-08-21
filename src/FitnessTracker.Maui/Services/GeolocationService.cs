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
                $"Moking Provider: {location.IsFromMockProvider}";
        }
    }

    public class MockGeolocationService : IGeolocationService
    {
        public string FormatLocation(Location location, Exception ex = null)
        {
            throw new NotImplementedException();
        }

        public Task<Location> GetLastKnownLocationAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Location> GetLocationAsync(GeolocationAccuracy Accuracy, CancellationTokenSource cts)
        {
            throw new NotImplementedException();
        }
    }
}
