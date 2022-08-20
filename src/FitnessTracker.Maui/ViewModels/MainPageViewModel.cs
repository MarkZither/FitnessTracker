using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessTracker.Maui.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        string notAvailable = "not available";
        string lastLocation;
        string currentLocation;
        int accuracy = (int)GeolocationAccuracy.Default;
        CancellationTokenSource cts;
        public MainPageViewModel()
        {
            BtnCounterText = "Click me";
            CounterClickedCommand = new RelayCommand(CounterClicked);
            GetLastLocationCommand = new Command(OnGetLastLocation);
            GetCurrentLocationCommand = new Command(OnGetCurrentLocation);
            OnGetCurrentLocation();
        }

        private int count;

        /// <summary>
        /// Gets or sets the name to display.
        /// </summary>
        public int Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        private TaskNotifier? myTask;

        /// <summary>
        /// Gets or sets the name to display.
        /// </summary>
        public Task? MyTask
        {
            get => myTask;
            private set => SetPropertyAndNotifyOnCompletion(ref myTask, value);
        }

        /// <summary>
        /// Gets the <see cref="ICommand"/> responsible for setting <see cref="MyTask"/>.
        /// </summary>
        public ICommand CounterClickedCommand { get; }
        public ICommand GetLastLocationCommand { get; }

        public ICommand GetCurrentLocationCommand { get; }
        public string LastLocation
        {
            get => lastLocation;
            set => SetProperty(ref lastLocation, value);
        }

        public string CurrentLocation
        {
            get => currentLocation;
            set => SetProperty(ref currentLocation, value);
        }

        public string[] Accuracies
            => Enum.GetNames(typeof(GeolocationAccuracy));

        public int Accuracy
        {
            get => accuracy;
            set => SetProperty(ref accuracy, value);
        }

        private string btnCounterText;

        /// <summary>
        /// Gets or sets the name to display.
        /// </summary>
        public string BtnCounterText
        {
            get => btnCounterText;
            set => SetProperty(ref btnCounterText, value);
        }

        public void CounterClicked()
        {
            Count++;

            if (Count == 1)
                BtnCounterText = $"Clicked {Count} time";
            else
                BtnCounterText = $"Clicked {Count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }

        async void OnGetLastLocation()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                LastLocation = FormatLocation(location);
            }
            catch (Exception ex)
            {
                LastLocation = FormatLocation(null, ex);
            }
            IsBusy = false;
        }
        async void OnGetCurrentLocation()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var request = new GeolocationRequest((GeolocationAccuracy)Accuracy);
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                CurrentLocation = FormatLocation(location);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                CurrentLocation = FormatLocation(null, fnsEx);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                CurrentLocation = FormatLocation(null, fneEx);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                CurrentLocation = FormatLocation(null, pEx);
            }
            catch (Exception ex)
            {
                CurrentLocation = FormatLocation(null, ex);
            }
            finally
            {
                cts.Dispose();
                cts = null;
            }
            IsBusy = false;
        }
        string FormatLocation(Location location, Exception ex = null)
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
        public override void OnDisappearing()
        {
            if (IsBusy)
            {
                if (cts != null && !cts.IsCancellationRequested)
                    cts.Cancel();
            }
            base.OnDisappearing();
        }
    }
}
