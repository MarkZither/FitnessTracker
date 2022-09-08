namespace FitnessTracker.Maui.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
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
}