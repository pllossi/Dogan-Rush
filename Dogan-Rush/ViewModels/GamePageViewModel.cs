using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.Infrastracture;
using Dogan_Rush.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Dogan_Rush.ViewModels
{
    public partial class GamePageViewModel : ObservableObject, INotifyPropertyChanged
    {
        
        private readonly GameManager _gameManager;

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

        public GamePageViewModel()
        {
            _gameManager = PreferenceUtilities.GetGame() ?? new GameManager();
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
                CurrentPersonImage = person.ImageData;

                if (!string.IsNullOrEmpty(CurrentPersonImage))
                {
                    if (CurrentPersonImage.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        PersonImage = ImageSource.FromUri(new Uri(CurrentPersonImage));
                    }
                    else
                    {
                        PersonImage = ImageSource.FromFile(Path.Combine("Resources", CurrentPersonImage));
                    }
                }
                else
                {
                    PersonImage = null;
                }

                IsIDDrawerVisible = false;
                IsVISADrawerVisible = false;

                Errors = _gameManager.ErrorsCounter;
                TurnCount = _gameManager.TurnCounter;
            }

            await Task.CompletedTask;
        }


        [RelayCommand]
        public async Task OnCorrectPressed()
        {
            _gameManager.Guess(true);
            await LoadNextPerson();
        }

        [RelayCommand]
        public async Task OnWrongPressed()
        {
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
    }
}