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

using NetTopologySuite.Geometries;

using NetTopologySuite.IO;

using SharpGPX;

using System.Text;

namespace FitnessTracker.Maui.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();

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
            CntViewMap.Content = mapControl;
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

        public static ILayer CreateLineStringLayer(IStyle? style = null)
        {
            using var stream = Task.Run(() => FileSystem.OpenAppPackageFileAsync("2022-05-02_20-01-31_-_walking.gpx")).GetAwaiter().GetResult();
            var src = GpxClass.FromStream(stream);
            StringBuilder sb = new StringBuilder("LINESTRING(");

            bool isFirstPoint = true;
            foreach (var item in src.Tracks[0].trkseg[0].trkpt)
            {
                if(!isFirstPoint)
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

        private const string WKTGr5 = "LINESTRING(49.621953 6.092523, 49.622953 6.093523, 49.623953 6.094523)";

    }
}