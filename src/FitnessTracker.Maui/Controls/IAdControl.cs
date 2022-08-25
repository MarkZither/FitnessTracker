using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Maui.Controls
{
    public interface IAdControl : IContentView
    {
        public string Text { get; }
        public Color TextColor { get; }
    }
}
