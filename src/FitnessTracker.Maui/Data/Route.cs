﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Maui.Data
{
    public class Route
    {
        public int id { get; set; }
        public IEnumerable<TrackerLocation> Locations { get; set; }
    }
}
