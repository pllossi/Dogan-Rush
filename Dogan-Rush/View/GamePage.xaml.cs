using Dogan_Rush.ViewModels;
using Dogan_Rush.Models;

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

        // Method to handle Correct button click
        private void OnCorrectClicked(object sender, EventArgs e)
        {
            _viewModel.Correct();  // Call the Correct method in the ViewModel
        }

        // Method to handle Incorrect button click
        private void OnIncorrectClicked(object sender, EventArgs e)
        {
            _viewModel.Incorrect();  // Call the Incorrect method in the ViewModel
        }
    }
}
