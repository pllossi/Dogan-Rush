using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.Infrastracture;
using Dogan_Rush.Models;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace Dogan_Rush.ViewModels
{
    public partial class GamePageViewModel : ObservableObject, INotifyPropertyChanged
    {
        private readonly GameManager _gameManager;
        private readonly string nullImageData = "person001.png";
        private bool _isFirstLoad = true;

        [ObservableProperty] private IDCard? currentIDCard;
        [ObservableProperty] private VISACard? currentVISACard;
        [ObservableProperty] private string? currentPersonImage;
        [ObservableProperty] private ImageSource? personImage;
        [ObservableProperty] private bool isIDDrawerVisible;
        [ObservableProperty] private bool isVISADrawerVisible;
        [ObservableProperty] private int errors;
        [ObservableProperty] private int turnCount;
        [ObservableProperty] private bool _errorShow = false;
        [ObservableProperty]
        private string? messageText;

        [ObservableProperty]
        private bool isMessageVisible;

        public GamePageViewModel()
        {
            _gameManager = PreferencesUtilities.GetGame() ?? new GameManager();
            if (_gameManager.CurrentPerson == null)
                _gameManager.NewTurn();
        }

        public GameManager GameManager => _gameManager;
        public Person? CurrentPerson => _gameManager.CurrentPerson;

        public async Task LoadNextPerson()
        {
            var person = _gameManager.CurrentPerson;
            if (person != null)
            {
                CurrentIDCard = person.IDCard;
                CurrentVISACard = person.VISACard;

                if (!string.IsNullOrWhiteSpace(person.ImageData))
                {
                    if (person.ImageData.StartsWith("http", System.StringComparison.OrdinalIgnoreCase))
                        PersonImage = ImageSource.FromUri(new Uri(person.ImageData));
                    else
                        CurrentPersonImage = !File.Exists(person.ImageData) ? person.ImageData : nullImageData;
                }
                else
                {
                    CurrentPersonImage = nullImageData;
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
           
            

            if(_gameManager.IsDocumentCorrect == true)
            {

                ErrorShow = true;
                OnPropertyChanged(nameof(ErrorShow));
                await Task.Delay(3000);
            }
            else
            {
                ErrorShow = false;
                OnPropertyChanged(nameof(ErrorShow));
            }

            if (_gameManager.GameStatus == GameStatus.Lose)
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

            ErrorShow = false;
            _gameManager.Guess(true);
            await LoadNextPerson();
            
        }

        [RelayCommand]
        public async Task OnWrongPressed()
        {
            

            if (_gameManager.IsDocumentCorrect == true)
            {
                ErrorShow = true;
                OnPropertyChanged(nameof(ErrorShow));
            }
            else
            {
                ErrorShow = false;
                OnPropertyChanged(nameof(ErrorShow));
            }

            if (_gameManager.GameStatus == GameStatus.Lose)
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

            ErrorShow = false;
            _gameManager.Guess(false);
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
