using FitnessTracker.Maui.Views;

namespace FitnessTracker.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("logviewer", typeof(LogViewerPage));
        }
    }
}