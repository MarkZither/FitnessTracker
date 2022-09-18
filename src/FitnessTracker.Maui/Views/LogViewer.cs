using Serilog.Sinks.InMemory;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Maui.Views
{
    public class LogViewer
    {
        private void ShowLogEvents()
        {
            var logEvents = InMemorySink.Instance
                    .LogEvents;
        }
    }
}
