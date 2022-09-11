using AppCenterExtensions;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using CommunityToolkit.Mvvm.DependencyInjection;

using FitnessTracker.Maui.Services;
using FitnessTracker.Maui.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using FitnessTracker.Maui.Configuration;

namespace FitnessTracker.Maui
{
    public partial class App : Application
    {
        private bool _initialized;
        public App()
        {
            InitializeComponent();

            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("FitnessTracker.Maui.appsettings.json");

            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .AddUserSecrets<App>()
                        .Build();

            var settings = config.GetRequiredSection("Settings").Get<FitnessSettings>();

            // Register services
            if (!_initialized)
            {
                _initialized = true;

                var serviceCollection = new ServiceCollection();
                //Services
                if (settings.MockGPS)
                {
                    serviceCollection.AddSingleton<IGeolocationService, MockGeolocationService>();
                }
                else
                {
                    serviceCollection.AddSingleton<IGeolocationService, GeolocationService>();
                }
                //.AddSingleton(RestService.For<IRedditService>("https://www.reddit.com/"))

                Ioc.Default.ConfigureServices(
                serviceCollection
                //ViewModels
                .AddTransient<MainPageViewModel>()
                .BuildServiceProvider());

                //AppCenterSetup.Instance.Start(
                //    //"[iOS AppCenter secret]",
                //    //"[Android AppCenter secret]",
                //    "uwp={Your app secret here};", // +
                ////"android={Your Android App secret here};" +
                ////"ios={Your iOS App secret here};" +
                ////"macos={Your macOS App secret here};",
                //true);


                AppCenter.Start(@$"uwp={settings.AppCenterWindowsDesktop};
                windowsdesktop={settings.AppCenterWindowsDesktop};", // +
                //"android={Your Android App secret here};" +
                //"ios={Your iOS App secret here};" +
                //"macos={Your macOS App secret here};",
                typeof(Analytics), typeof(Crashes));
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