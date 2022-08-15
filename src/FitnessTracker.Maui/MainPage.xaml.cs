using Mapsui;
using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
using Mapsui.Widgets.Zoom;

namespace FitnessTracker.Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            var mapControl = new Mapsui.UI.Maui.MapControl();
            mapControl.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
            // Get the lon lat coordinates from somewhere (Mapsui can not help you there)
            var whereYouAre = new MPoint(6.092523, 49.621953);
            // OSM uses spherical mercator coordinates. So transform the lon lat coordinates to spherical mercator
            var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(whereYouAre.X, whereYouAre.Y).ToMPoint();
            // Set the center of the viewport to the coordinate. The UI will refresh automatically
            // Additionally you might want to set the resolution, this could depend on your specific purpose
            mapControl.Map.Home = n => n.NavigateTo(sphericalMercatorCoordinate, mapControl.Map.Resolutions[15]);
            mapControl.Map.Widgets.Add(new ScaleBarWidget(mapControl.Map) { TextAlignment = Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
            mapControl.Map.Widgets.Add(new ZoomInOutWidget { MarginX = 20, MarginY = 40 });
            CntViewMap.Content = mapControl;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}