using FitnessTracker.Maui.Services;
using FitnessTracker.Maui.ViewModels;

namespace FitnessTracker.Tests.ViewModels
{
    public class MainPageViewModelTests
    {
        private IGeolocationService subGeolocationService;

        public MainPageViewModelTests()
        {
            this.subGeolocationService = Substitute.For<IGeolocationService>();
        }

        private MainPageViewModel CreateViewModel()
        {
            return new MainPageViewModel(
                this.subGeolocationService);
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
