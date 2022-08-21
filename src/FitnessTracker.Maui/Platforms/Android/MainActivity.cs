using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace FitnessTracker.Maui
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Microsoft.Maui.ApplicationModel.Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle
            Microsoft.Maui.ApplicationModel.Platform.ActivityStateChanged += Platform_ActivityStateChanged;
        }
        protected override void OnResume()
        {
            base.OnResume();

            Microsoft.Maui.ApplicationModel.Platform.OnResume(this);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            Microsoft.Maui.ApplicationModel.Platform.OnNewIntent(intent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Microsoft.Maui.ApplicationModel.Platform.ActivityStateChanged -= Platform_ActivityStateChanged;
        }
        void Platform_ActivityStateChanged(object sender, Microsoft.Maui.ApplicationModel.ActivityStateChangedEventArgs e) =>
            Toast.MakeText(this, e.State.ToString(), ToastLength.Short).Show();

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Microsoft.Maui.ApplicationModel.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}