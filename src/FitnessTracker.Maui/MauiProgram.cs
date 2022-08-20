using SkiaSharp.Views.Maui.Controls.Hosting;

namespace FitnessTracker.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
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

            return builder.Build();
        }
    }
}