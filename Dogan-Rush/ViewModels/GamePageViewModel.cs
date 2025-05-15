using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dogan_Rush.Models;
using Dogan_Rush.Infrastracture;
using Microsoft.Maui.Graphics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Dogan_Rush.ViewModels
{
    public partial class GamePageViewModel : ObservableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private GameManager _gameManager;
        private ImageSource _personImage;
        [ObservableProperty]
        public bool _isIDDrawerVisible;
        [ObservableProperty]
        public bool _isVISADrawerVisible;

        [ObservableProperty]
        public string _currentPersonImage;
        [ObservableProperty]
        public int _errors;
        [ObservableProperty]
        public int _turn;
        [ObservableProperty]
        public VISACard _currentVISACard;
        [ObservableProperty]
        public IDCard _currentIDCard;

        public GamePageViewModel()
        {
            // Prova a recuperare il salvataggio
            _gameManager = PreferenceUtilities.GetGame() ?? new GameManager();
        }

        public GameManager GameManager => _gameManager;

        public ImageSource PersonImage
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
        

        private bool _IsIDDrawerVisible_
        {
           
            set
            {
                _isIDDrawerVisible = value;
                OnPropertyChanged();
            }
        }


        public bool IsVISADrawerVisible_
        {
            set
            {
                _isVISADrawerVisible = value;
                OnPropertyChanged();
            }
        }

        public Person CurrentPerson => _gameManager.CurrentPerson;

        public ICommand GuessCorrectCommand { get; }
        public ICommand GuessWrongCommand { get; }

        public ICommand ToggleIDDrawerCommand { get; }
        public ICommand ToggleVISADrawerCommand { get; }

        public void LoadNextPerson()
        {
            _gameManager.NewTurn();

            _currentIDCard = _gameManager.CurrentPerson.IDCard;
            _currentVISACard = _gameManager.CurrentPerson.VISACard;

            OnPropertyChanged(nameof(CurrentPerson));
            OnPropertyChanged(nameof(ErrorCountViewModel));
            OnPropertyChanged(nameof(TurnCount));

            // Caricamento immagine dal campo ImageData
            if (!string.IsNullOrEmpty(CurrentPerson.ImageData))
            {
                if (CurrentPerson.ImageData.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    PersonImage = ImageSource.FromUri(new Uri(CurrentPerson.ImageData));
                }
                else
                {
                    PersonImage = ImageSource.FromFile(CurrentPerson.ImageData);
                }
            }

            // Chiude i cassetti ad ogni turno
            _isIDDrawerVisible = false;
            _isVISADrawerVisible = false;
        }

        [RelayCommand]
        public async Task OnCorrectPressed()
        {
            _gameManager.Guess(true);
            LoadNextPerson();
        }

        [RelayCommand]
        public async Task OnWrongPressed()
        {
            _gameManager.Guess(false);
            LoadNextPerson();
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


       


    }
}
