using CommunityToolkit.Mvvm.DependencyInjection;

using FitnessTracker.Maui.ViewModels;

using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Nts.Extensions;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
using Mapsui.Widgets.Zoom;

using Microsoft.Maui.Platform;

using NetTopologySuite.Geometries;

using NetTopologySuite.IO;

using SharpGPX;

using System.Text;
using System.Threading;

namespace FitnessTracker.Maui.Views
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel ViewModel;
        public MainPage()
        {
            InitializeComponent();
            //BindingContext = mainPageViewModel;
            BindingContext = ViewModel = Ioc.Default.GetRequiredService<MainPageViewModel>();

            var mapControl = new Mapsui.UI.Maui.MapControl();
            mapControl.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer("Marks.Fitness.MAUI.App"));
            // Get the lon lat coordinates from somewhere (Mapsui can not help you there)
            var whereYouAre = new MPoint(6.092523, 49.621953);
            // OSM uses spherical mercator coordinates. So transform the lon lat coordinates to spherical mercator
            var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(whereYouAre.X, whereYouAre.Y).ToMPoint();
            // Set the center of the viewport to the coordinate. The UI will refresh automatically
            // Additionally you might want to set the resolution, this could depend on your specific purpose
            mapControl.Map.Home = n => n.NavigateTo(sphericalMercatorCoordinate, mapControl.Map.Resolutions[15]);
            mapControl.Map.Widgets.Add(new ScaleBarWidget(mapControl.Map) { TextAlignment = Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
            mapControl.Map.Widgets.Add(new ZoomInOutWidget { MarginX = 20, MarginY = 40 });
            var lineStringLayer = CreateLineStringLayer(CreateLineStringStyle());
            mapControl.Map.Layers.Add(lineStringLayer);
            ILayer layer = null; // holds the eventual result
            var apiTask = new Task(() => layer = CreatePointLayerAsync().Result); // creates the task with the call on another thread
            apiTask.Start(); // starts the task - important, or you'll spin forever
            Task.WaitAll(apiTask); // waits for it to complete
            mapControl.Map.Layers.Add(layer);
            mapControl.Map.Layers.Add(CreateMutatingTriangleLayer(mapControl.Map.Extent));
            CntViewMap.Content = mapControl;
            ModifyAd();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        private async Task<MemoryLayer> CreatePointLayerAsync()
        {
            return new MemoryLayer
            {
                Name = "Points",
                IsMapInfoLayer = true,
                Features = await GetLocationAsync(),
                Style = new LabelStyle { Text = "Default Label" }
            };
        }

        private async Task<IEnumerable<IFeature>> GetLocationAsync()
        {
            var features = new List<IFeature>();

            if (ViewModel.CurrentLocation2 != null)
            {
                features.Add(new PointFeature(SphericalMercator.FromLonLat(ViewModel.CurrentLocation2.Longitude, ViewModel.CurrentLocation2.Latitude).ToMPoint()));
            }
            else if (ViewModel.LastLocation2 != null)
            {
                features.Add(new PointFeature(SphericalMercator.FromLonLat(ViewModel.LastLocation2.Longitude, ViewModel.LastLocation2.Latitude).ToMPoint()));
            }

            return features;
        }
        public static IStyle CreateLineStringStyle()
        {
            return new VectorStyle
            {
                Fill = null,
                Outline = null,
#pragma warning disable CS8670 // Object or collection initializer implicitly dereferences possibly null member.
                Line = { Color = Mapsui.Styles.Color.FromString("Red"), Width = 8 }
            };
        }

        private static ILayer CreateMutatingTriangleLayer(MRect? envelope)
        {
            var layer = new MemoryLayer();

            var polygon = new Polygon(new LinearRing(GenerateRandomPoints(envelope, 3).ToArray()));
            var feature = new GeometryFeature(polygon);
            layer.Features = new List<IFeature> { feature };

            PeriodicTask.RunAsync(() =>
            {
                feature.Geometry = new Polygon(new LinearRing(GenerateRandomPoints(envelope, 3).ToArray()));
                // Clear cache for change to show
                feature.RenderedGeometry.Clear();
                // Trigger DataChanged notification
                layer.DataHasChanged();
            },
            TimeSpan.FromMilliseconds(1000));

            return layer;
        }

        private static readonly Random Random = new Random(0);
        public static IEnumerable<Coordinate> GenerateRandomPoints(MRect? envelope, int count = 25)
        {
            var result = new List<Coordinate>();
            if (envelope == null)
                return result;

            for (var i = 0; i < count; i++)
            {
                result.Add(new Coordinate(
                    Random.NextDouble() * envelope.Width + envelope.Left,
                    Random.NextDouble() * envelope.Height + envelope.Bottom));
            }

            result.Add(result[0].Copy()); // close polygon by adding start point.

            return result;
        }

        public static ILayer CreateLineStringLayer(IStyle? style = null)
        {
            using var stream = Task.Run(() => FileSystem.OpenAppPackageFileAsync("2022-05-02_20-01-31_-_walking.gpx")).GetAwaiter().GetResult();
            var src = GpxClass.FromStream(stream);
            StringBuilder sb = new StringBuilder("LINESTRING(");

            bool isFirstPoint = true;
            foreach (var item in src.Tracks[0].trkseg[0].trkpt)
            {
                if (!isFirstPoint)
                {
                    sb.Append(", ");
                }
                sb.Append($"{item.lat} {item.lon}");
                isFirstPoint = false;
            }
            sb.Append(')');
            var lineStringFromBuilder = sb.ToString();
            var lineString = (LineString)new WKTReader().Read(lineStringFromBuilder);
            lineString = new LineString(lineString.Coordinates.Select(v => SphericalMercator.FromLonLat(v.Y, v.X).ToCoordinate()).ToArray());

            return new MemoryLayer
            {
                Features = new[] { new GeometryFeature { Geometry = lineString } },
                Name = "LineStringLayer",
                Style = style

            };
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
            handler.PlatformView.Background = (Colors.Blue.ToPlatform());
            //handler.PlatformView.FontWeight = Microsoft.UI.Text.FontWeights.Thin;
#endif
            });
        }

        private static CancellationTokenSource? _cancelationTokenSource;
        public class PeriodicTask
        {
            public static async Task RunAsync(Action action, TimeSpan period, CancellationToken? cancellationToken)
            {
                while (!(cancellationToken?.IsCancellationRequested ?? false))
                {
                    if (cancellationToken == null)
                    {
                        await Task.Delay(period);
                    }
                    else
                    {
                        await Task.Delay(period, cancellationToken.Value);
                    }

                    if (!(cancellationToken?.IsCancellationRequested ?? false))
                        action();
                }
            }

            public static Task RunAsync(Action action, TimeSpan period)
            {
                return RunAsync(action, period, _cancelationTokenSource?.Token);
            }
        }
    }
}