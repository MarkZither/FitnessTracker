

## MVVM
https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/

## Dependency Injection
https://www.syncfusion.com/blogs/post/learn-how-to-use-dependency-injection-in-net-maui.aspx
https://github.com/CommunityToolkit/MVVM-Samples/blob/master/samples/MvvmSampleXF/MvvmSampleXF/App.xaml.cs

## Essentials
Getting started with MAUI essentials
https://docs.microsoft.com/en-us/xamarin/essentials/get-started?tabs=windows%2Candroid
https://github.com/dotnet/maui/blob/main/src/Essentials/samples/Samples/Platforms/Android/MainActivity.cs
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


## Handler-Mapper Pattern Instead of Customer Renderers
https://github.com/dotnet/maui/wiki/Porting-Custom-Renderers-to-Handlers
https://www.syncfusion.com/blogs/post/how-to-customize-net-maui-controls-with-handler-architecture.aspx
https://devblogs.microsoft.com/dotnet/customizing-dotnet-maui-controls/

## Unit Test Project Setup
https://github.com/dotnet/maui/discussions/5828
https://github.com/dotnet/maui/issues/2112

### FitnessTracker.Maui.csproj
Added .net6.0 to `<TargetFrameworks>net6.0;net6.0-android</TargetFrameworks>` 
and
```
    <PropertyGroup Condition="'$(TargetFramework)' == 'net6.0'">
        <OutputType>Library</OutputType>
    </PropertyGroup>
    ```
in FitnessTracker.Maui.csproj.

### FitnessTracker.Tests.csproj
Added `<UseMaui>true</UseMaui>` to FitnessTracker.Tests.csproj

## Configuration
This is mostly for debugging and avoiding hardcoding secrets and having to commit them to the git repo.
https://montemagno.com/dotnet-maui-appsettings-json-configuration/

## Toast and Snackbars
https://vladislavantonyuk.azurewebsites.net/articles/How-to-show-SnackBar-and-Toast-in-.NET-MAUI


# DataContext
`dotnet ef migrations add InitialMigration -o Data\Migrations`