using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Maui.Configuration
{

    public class FitnessSettings
    {
        public string AppCenterWindowsDesktop { get; set; }
        public bool MockGPS { get; set; }
        public NestedSettings KeyThree { get; set; } = null!;
    }

    public class NestedSettings
    {
        public string Message { get; set; } = null!;
    }
}
