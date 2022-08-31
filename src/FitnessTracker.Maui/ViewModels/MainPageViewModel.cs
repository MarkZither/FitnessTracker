﻿using CommunityToolkit.Mvvm.Input;

using FitnessTracker.Maui.Services;

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
        string lastLocation;
        Location lastLocation2;
        string currentLocation;
        Location currentLocation2;
        private List<Location> TrackedLocations { get; set; } = new();
        int accuracy = (int)GeolocationAccuracy.Default;
        CancellationTokenSource cts;
        private readonly IGeolocationService _geolocationService;
        public MainPageViewModel(IGeolocationService geolocationService)
        {
            _geolocationService = geolocationService;
            BtnCounterText = "Click me";
            BtnStartStopText = "Start";
            CounterClickedCommand = new RelayCommand(CounterClicked);
            StartStopClickedCommand = new RelayCommand(StartStopClicked);
            GetLastLocationCommand = new Command(OnGetLastLocation);
            GetCurrentLocationCommand = new Command(OnGetCurrentLocation);
            OnGetLastLocation();
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
        public ICommand StartStopClickedCommand { get; }
        public ICommand GetLastLocationCommand { get; }
        public ICommand GetCurrentLocationCommand { get; }
        public string LastLocation
        {
            get => lastLocation;
            set => SetProperty(ref lastLocation, value);
        }

        public Location LastLocation2
        {
            get => lastLocation2;
            set => SetProperty(ref lastLocation2, value);
        }
        public string CurrentLocation
        {
            get => currentLocation;
            set => SetProperty(ref currentLocation, value);
        }

        public Location CurrentLocation2
        {
            get => currentLocation2;
            set => SetProperty(ref currentLocation2, value);
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

        private string btnStartStopText;

        /// <summary>
        /// Gets or sets the name to display.
        /// </summary>
        public string BtnStartStopText
        {
            get => btnStartStopText;
            set => SetProperty(ref btnStartStopText, value);
        }

        private bool isRunning;

        /// <summary>
        /// Gets or sets the name to display.
        /// </summary>
        public bool IsRunning
        {
            get => isRunning;
            set => SetProperty(ref isRunning, value);
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

        public void StartStopClicked()
        {
            if (!isRunning)
            {
                BtnStartStopText = "Stop";
                isRunning = !isRunning;
                var _ = Task.Run(() => TrackLocations());
            }
            else
            {
                BtnStartStopText = "Start";
                isRunning = !isRunning;
            }

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }

        void TrackLocations()
        {
            while(isRunning)
            {
                OnGetCurrentLocation();
            }
        }

        async void OnGetLastLocation()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Location location = await _geolocationService.GetLastKnownLocationAsync();
                LastLocation2 = location;
                LastLocation = _geolocationService.FormatLocation(location);
            }
            catch (Exception ex)
            {
                LastLocation = _geolocationService.FormatLocation(null, ex);
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
                cts = new CancellationTokenSource();
                Location location = await _geolocationService.GetLocationAsync((GeolocationAccuracy)accuracy,cts);
                currentLocation2 = location;
                CurrentLocation = _geolocationService.FormatLocation(location);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                CurrentLocation = _geolocationService.FormatLocation(null, fnsEx);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                CurrentLocation = _geolocationService.FormatLocation(null, fneEx);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                CurrentLocation = _geolocationService.FormatLocation(null, pEx);
            }
            catch (Exception ex)
            {
                CurrentLocation = _geolocationService.FormatLocation(null, ex);
            }
            finally
            {
                cts.Dispose();
                cts = null;
            }
            IsBusy = false;
            if(isRunning)
            {
                TrackedLocations.Add(CurrentLocation2);
            }
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
