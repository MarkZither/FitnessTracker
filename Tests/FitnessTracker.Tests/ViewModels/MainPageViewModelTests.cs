using FitnessTracker.Maui.Services;
using FitnessTracker.Maui.ViewModels;

using Microsoft.Extensions.Logging;

using Serilog;

namespace FitnessTracker.Tests.ViewModels
{
    public class MainPageViewModelTests
    {
        private IGeolocationService subGeolocationService;
        private ILogger<MainPageViewModel> _logger;

        public MainPageViewModelTests()
        {
            this.subGeolocationService = Substitute.For<IGeolocationService>();
            this._logger = Substitute.For<ILogger<MainPageViewModel>>();
        }

        private MainPageViewModel CreateViewModel()
        {
            return new MainPageViewModel(
                this.subGeolocationService, _logger);
        }

        [Fact]
        public void CounterClicked_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var viewModel = this.CreateViewModel();

            // Act
            viewModel.CounterClicked();

            // Assert
            Assert.True(viewModel.Count.Equals(1));
        }
    }
}
