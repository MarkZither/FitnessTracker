using CommunityToolkit.Mvvm.DependencyInjection;

using FitnessTracker.Maui.ViewModels;

namespace FitnessTracker.Maui.Views;

public partial class SettingsPage : ContentPage
{
    readonly SettingsViewModel ViewModel;
    public SettingsPage()
	{
		InitializeComponent();
        BindingContext = ViewModel = Ioc.Default.GetRequiredService<SettingsViewModel>();
    }

    async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
    {
        // Handle the tap
        string result = await DisplayPromptAsync("Use Demo Mode?", "What's your name?");
    }

    async void OnClickGestureRecognizerTapped(object sender, EventArgs args)
    {
        // Handle the tap
        string result = await DisplayPromptAsync("Use Demo Mode?", "What's your name?");
    }
    async void OnButtonClicked(object sender, EventArgs args)
    {
        await lblTest.RelRotateTo(360, 1000);
        await Shell.Current.GoToAsync("//logviewer");
    }
}