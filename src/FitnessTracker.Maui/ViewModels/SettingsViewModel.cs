using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessTracker.Maui.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            OpenLogClickedCommand = new AsyncRelayCommand(OpenLog);
        }

        /// <summary>
        /// Gets the <see cref="ICommand"/> responsible for setting <see cref="MyTask"/>.
        /// </summary>
        public ICommand OpenLogClickedCommand { get; }

        public async Task OpenLog()
        {
            await Shell.Current.GoToAsync("//logviewer");
        }
    }
}
