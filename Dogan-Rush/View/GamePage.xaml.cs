using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.ViewModels;

namespace Dogan_Rush.View
{
    public partial class GamePage : ContentPage
    {
        // ViewModel to be used in this page
        private GamePageViewModel _viewModel;

        public GamePage()
        {
            InitializeComponent();
            _viewModel = new GamePageViewModel();  // Initialize the ViewModel
            BindingContext = _viewModel;  // Bind the ViewModel to the page
        }

        [RelayCommand]
        private async void OnCorrectClicked()
        {
            await _viewModel.OnCorrectPressed();
            // Call the Correct method in the ViewModel
        }

        [RelayCommand]
        private async void OnIncorrectClicked()
        {
            await _viewModel.OnWrongPressed();
            // Call the Incorrect method in the ViewModel
        }
    }
}