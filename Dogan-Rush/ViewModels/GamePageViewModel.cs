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
        public event PropertyChangedEventHandler? PropertyChanged;

        private GameManager? _gameManager;
        private ImageSource? _personImage;

        [ObservableProperty]
        public bool? _isIDDrawerVisible;

        [ObservableProperty]
        public bool? _isVISADrawerVisible;

        [ObservableProperty]
        public string? _currentPersonImage;

        [ObservableProperty]
        public int? _errors;

        [ObservableProperty]
        public int? _turn;

        [ObservableProperty]
        public VISACard? _currentVISACard;

        [ObservableProperty]
        public IDCard? _currentIDCard;

        public GamePageViewModel()
        {
            // Prova a recuperare il salvataggio
            _gameManager = PreferenceUtilities.GetGame() ?? new GameManager();
            if (PreferenceUtilities.GetGame() == null)
                _gameManager = new GameManager();
            _gameManager.NewTurn();
        }

        public GameManager GameManager => _gameManager;

        public ImageSource? PersonImage
        {
            get => _personImage;
            set
            {
                _personImage = value;
                OnPropertyChanged();
            }
        }

        public int ErrorCountViewModel => 3 - _gameManager.LifesCounter;
        public int TurnCount => _gameManager.TurnCounter;

        private bool? IsIDDrawerVisible_
        {
            set
            {
                IsIDDrawerVisible = value;
                OnPropertyChanged();
            }
        }

        public bool? IsVISADrawerVisible_
        {
            set
            {
                IsVISADrawerVisible = value;
                OnPropertyChanged();
            }
        }

        public Person? CurrentPerson => _gameManager.CurrentPerson;

        public void LoadNextPerson()
        {
            CurrentIDCard = _gameManager.CurrentPerson.IDCard;
            CurrentVISACard = _gameManager.CurrentPerson.VISACard;

            CurrentPersonImage = _gameManager.CurrentPerson.ImageData;
            Errors = 3 - _gameManager.LifesCounter;

            OnPropertyChanged(nameof(CurrentPerson));
            OnPropertyChanged(nameof(ErrorCountViewModel));
            OnPropertyChanged(nameof(TurnCount));
            OnPropertyChanged(nameof(CurrentIDCard));
            OnPropertyChanged(nameof(CurrentVISACard));
            OnPropertyChanged(nameof(CurrentPersonImage));
            OnPropertyChanged(nameof(PersonImage));
            OnPropertyChanged(nameof(IsIDDrawerVisible_));
            OnPropertyChanged(nameof(IsVISADrawerVisible_));

            // Caricamento immagine dal campo ImageData
            if (!string.IsNullOrEmpty(CurrentPerson.ImageData))
            {
                if (CurrentPerson.ImageData.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    PersonImage = ImageSource.FromUri(new Uri(CurrentPerson.ImageData));
                }
                else
                {
                    PersonImage = ImageSource.FromFile("\\Resources\\" + CurrentPerson.ImageData);
                }
            }

            // Chiude i cassetti ad ogni turno
            IsIDDrawerVisible = false;
            IsVISADrawerVisible = false;
        }

        [RelayCommand]
        public async Task OnCorrectPressed()
        {
            if (_gameManager == null)
                return;
            _gameManager.Guess(true);
            LoadNextPerson();
        }

        [RelayCommand]
        public async Task OnWrongPressed()
        {
            if (_gameManager == null)
                return;
            _gameManager.Guess(false);
            LoadNextPerson();
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        [RelayCommand]
        public void ToggleIDDrawer()
        {
            IsIDDrawerVisible_ = !IsIDDrawerVisible;
            IsVISADrawerVisible_ = !IsIDDrawerVisible;
        }

        [RelayCommand]
        public void ToggleVISADrawer()
        {
            IsVISADrawerVisible_ = !IsVISADrawerVisible;
            IsIDDrawerVisible_ = !IsVISADrawerVisible;
        }
    }
}