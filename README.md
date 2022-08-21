

## MVVM
https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/

## Essentials
Getting started with MAUI essentials
https://docs.microsoft.com/en-us/xamarin/essentials/get-started?tabs=windows%2Candroid
```
 protected override void OnCreate(Bundle? savedInstanceState)
 {
     Microsoft.Maui.ApplicationModel.Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle

     base.OnCreate(savedInstanceState);
 }

 public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
 {
     Microsoft.Maui.ApplicationModel.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

     base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
 }
```

## Geolocation
https://github.com/dotnet/maui/blob/main/src/Essentials/samples/Samples/ViewModel/GeolocationViewModel.cs

### Unexpected error when location is turned off on Windows
https://github.com/xamarin/Essentials/issues/819