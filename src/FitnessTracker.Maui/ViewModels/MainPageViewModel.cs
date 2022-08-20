using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessTracker.Maui.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            BtnCounterText = "Click me";
            CounterClickedCommand = new RelayCommand(CounterClicked);
        }

        private int count;

        /// <summary>
        /// Gets or sets the name to display.
        /// </summary>
        public int Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        private TaskNotifier? myTask;

        /// <summary>
        /// Gets or sets the name to display.
        /// </summary>
        public Task? MyTask
        {
            get => myTask;
            private set => SetPropertyAndNotifyOnCompletion(ref myTask, value);
        }

        /// <summary>
        /// Gets the <see cref="ICommand"/> responsible for setting <see cref="MyTask"/>.
        /// </summary>
        public ICommand CounterClickedCommand { get; }
        
        private string btnCounterText;

        /// <summary>
        /// Gets or sets the name to display.
        /// </summary>
        public string BtnCounterText
        {
            get => btnCounterText;
            set => SetProperty(ref btnCounterText, value);
        }

        public void CounterClicked()
        {
            Count++;

            if (Count == 1)
                BtnCounterText = $"Clicked {Count} time";
            else
                BtnCounterText = $"Clicked {Count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }

    }
}
