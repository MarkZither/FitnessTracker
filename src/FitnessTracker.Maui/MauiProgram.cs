using FitnessTracker.Maui.Services;
using FitnessTracker.Maui.ViewModels;

using Microsoft.Extensions.Configuration;
using CommunityToolkit.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;

using System.Reflection;

namespace FitnessTracker.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureEssentials(essentials =>
                {
                    essentials.UseVersionTracking();
                    essentials.AddAppAction("app_info", "App Info", icon: "app_info_action_icon");
                    essentials.AddAppAction("battery_info", "Battery Info");
                    essentials.OnAppAction(App.HandleAppActions);
                });

            //builder.Services.AddSingleton<IGeolocationService, GeolocationService>()
            //    .AddTransient<MainPageViewModel>();
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("FitnessTracker.Maui.appsettings.json");

            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .AddUserSecrets<App>()
                        .Build();


            builder.Configuration.AddConfiguration(config);

            return builder.Build();
        }
    }
}