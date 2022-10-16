using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace FitnessTracker.Maui.Views;

public partial class LogViewerPage : ContentPage
{
    private readonly ILogger<LogViewerPage> _logger;
    public LogViewerPage()
	{
		InitializeComponent();
        _logger = Ioc.Default.GetRequiredService<ILogger<LogViewerPage>>();
        _logger.LogWarning("warning");
        var events = Serilog.Sinks.InMemory.InMemorySink.Instance.LogEvents;
        foreach (var item in events)
        {
            editor.Text = item.MessageTemplate.Text;
            editor.Text += Environment.NewLine;
        }
        
    }
}