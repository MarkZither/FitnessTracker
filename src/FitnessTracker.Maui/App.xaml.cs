using Android.Service.QuickSettings;

using CommunityToolkit.Mvvm.DependencyInjection;

using FitnessTracker.Maui.Services;
using FitnessTracker.Maui.ViewModels;

namespace FitnessTracker.Maui
{
    public partial class App : Application
    {
        private bool _initialized;
        public App()
        {
            InitializeComponent();

            // Register services
            if (!_initialized)
            {
                _initialized = true;

                Ioc.Default.ConfigureServices(
                new ServiceCollection()
                //Services
                .AddSingleton<IGeolocationService, GeolocationService>()
                //.AddSingleton(RestService.For<IRedditService>("https://www.reddit.com/"))
                //ViewModels
                .AddTransient<MainPageViewModel>()
                .BuildServiceProvider());
            }

            MainPage = new AppShell();
        }

        public static void HandleAppActions(AppAction appAction)
        {
            App.Current.Dispatcher.Dispatch(async () =>
            {
                var page = appAction.Id switch
                {
                    //"battery_info" => new BatteryPage(),
                    //"app_info" => new AppInfoPage(),
                    _ => default(Page)
                };

                if (page != null)
                {
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }
            });
        }
    }
}