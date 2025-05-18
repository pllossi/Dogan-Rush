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
            BindingContext = _viewModel = new GamePageViewModel();

            // Load the first person immediately
            Loaded += async (_, _) => await _viewModel.LoadNextPerson();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadNextPerson(); // Proper async call to initialize data
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