using Dogan_Rush.ViewModels;
using Dogan_Rush.Infrastracture;
using Dogan_Rush.Models;
using CommunityToolkit.Mvvm.Input;

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
        // Method to handle Correct button click
        private void OnCorrectClicked()
        {
            _viewModel.OnCorrectPressed();  // Call the Correct method in the ViewModel
        }

        [RelayCommand]
        // Method to handle Incorrect button click
        private void OnIncorrectClicked()
        {
            _viewModel.OnWrongPressed();  // Call the Incorrect method in the ViewModel
        }
    }
}
