using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.Infrastracture;
using Dogan_Rush.ViewModels;

namespace Dogan_Rush.View
{
    public partial class GamePage : ContentPage
    {
        // ViewModel to be used in this page
        private GamePageViewModel _viewModel;

        private MusicPlayer _musicPlayer;

        public GamePage(bool reset = false)
        {
            InitializeComponent();
            if (reset)
            {
                PreferencesUtilities.ClearGame();
            }
            BindingContext = _viewModel = new GamePageViewModel();
            _musicPlayer = new MusicPlayer();
            _musicPlayer.PlayMusic();

            // Load the first person immediately
            Loaded += async (_, _) => await _viewModel.LoadNextPerson();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is GamePageViewModel vm)
            {
                await vm.LoadNextPerson();
            }
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as GamePageViewModel;

            if (viewModel?.GameManager != null)
            {
                PreferencesUtilities.SaveGame(viewModel.GameManager);
            }
        }


    }
}