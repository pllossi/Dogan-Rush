using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.Infrastracture;
using Dogan_Rush.Models;
using System.ComponentModel;

namespace Dogan_Rush.ViewModels
{
    public partial class GamePageViewModel : ObservableObject, INotifyPropertyChanged
    {
        private readonly GameManager? _gameManager;

        [ObservableProperty]
        private IDCard? currentIDCard;

        [ObservableProperty]
        private VISACard? currentVISACard;

        [ObservableProperty]
        private string? currentPersonImage;

        [ObservableProperty]
        private ImageSource? personImage;

        [ObservableProperty]
        private bool isIDDrawerVisible;

        [ObservableProperty]
        private bool isVISADrawerVisible;

        [ObservableProperty]
        private int errors;

        [ObservableProperty]
        private int turnCount;

        [ObservableProperty]
        private string? messageText;

        [ObservableProperty]
        private bool isMessageVisible;


        private string _nullImageData = "person001.png";

        public GamePageViewModel()
        {
            _gameManager = PreferencesUtilities.GetGame();
            if (_gameManager == null)
            {
                _gameManager = new GameManager();
                _gameManager.NewTurn();
            }
        }

        public GameManager GameManager => _gameManager;

        public Person? CurrentPerson => GameManager.CurrentPerson;

        public async Task LoadNextPerson()
        {
            
            if (_gameManager.CurrentPerson != null)
            {
                CurrentIDCard = _gameManager.CurrentPerson.IDCard;
                CurrentVISACard = _gameManager.CurrentPerson.VISACard;

                if (!string.IsNullOrEmpty(_gameManager.CurrentPerson.ImageData))
                {
                    if (_gameManager.CurrentPerson.ImageData.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        PersonImage = ImageSource.FromUri(new Uri(_gameManager.CurrentPerson.ImageData));
                    }
                    else
                    {
                        if (File.Exists(_gameManager.CurrentPerson.ImageData) == true)
                        {
                            CurrentPersonImage = _nullImageData;
                        }
                        else
                        {
                            CurrentPersonImage = _gameManager.CurrentPerson.ImageData;
                        }
                    }
                }
                else
                {
                    CurrentPersonImage = _nullImageData;
                }

                IsIDDrawerVisible = false;
                IsVISADrawerVisible = false;

                Errors = _gameManager.ErrorsCounter;
                TurnCount = _gameManager.TurnCounter;
                if (TurnCount % 5 == 0 && TurnCount > 1)
                    PreferencesUtilities.SaveGame(_gameManager);
            }

            await Task.CompletedTask;
        }

        [RelayCommand]
        public async Task OnCorrectPressed()
        {
            _gameManager.Guess(true);
            if(_gameManager.GameStatus == GameStatus.Lose)
            {
                ShowMessage("Hai Perso");
                PreferencesUtilities.ClearGame();
                return;
            }
            else if (_gameManager.GameStatus == GameStatus.Win)
            {
                ShowMessage("Congratulations! You won the game.");
                PreferencesUtilities.ClearGame();
                return;
            }
            await LoadNextPerson();
        }

        [RelayCommand]
        public async Task OnWrongPressed()
        {
            _gameManager.Guess(false);
            if(_gameManager.GameStatus == GameStatus.Lose)
            {
                ShowMessage("Hai Perso");
                PreferencesUtilities.ClearGame();
                return;
            }
            else if (_gameManager.GameStatus == GameStatus.Win)
            {
                ShowMessage("Congratulations! You won the game.");
                PreferencesUtilities.ClearGame();
                return;
            }
            await LoadNextPerson();
        }

        [RelayCommand]
        public void ToggleIDDrawer()
        {
            IsIDDrawerVisible = !IsIDDrawerVisible;
            IsVISADrawerVisible = false;
        }

        [RelayCommand]
        public void ToggleVISADrawer()
        {
            IsVISADrawerVisible = !IsVISADrawerVisible;
            IsIDDrawerVisible = false;
        }

        public async void ShowMessage(string text)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                MessageText = text;
                IsMessageVisible = true;
            });

            await Task.Delay(2000);

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                IsMessageVisible = false;
            });
        }

    }
}