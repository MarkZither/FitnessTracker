using Microsoft.Maui.Platform;

namespace FitnessTracker.Maui.Controls;

public partial class AdControl : ContentView
{
	public AdControl()
	{
		InitializeComponent();
        ModifyAd();
    }

    private void ModifyAd()
    {
        Microsoft.Maui.Handlers.ContentViewHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Colors.DeepPink.ToPlatform());
#elif IOS
            handler.PlatformView.SetBackgroundColor(Colors.Green.ToPlatform());
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
            //handler.PlatformView.SetBackgroundColor(Colors.Blue.ToPlatform());
            //handler.PlatformView.FontWeight = Microsoft.UI.Text.FontWeights.Thin;
#endif
        });
    }
}