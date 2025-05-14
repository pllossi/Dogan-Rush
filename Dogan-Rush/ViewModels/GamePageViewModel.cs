using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dogan_Rush.Models;
using Dogan_Rush.Infrastracture;
using Microsoft.Maui.Graphics;

namespace Dogan_Rush.ViewModels
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private GameManager _gameManager;
        private ImageSource _personImage;
        private bool _isIDDrawerVisible;
        private bool _isVISADrawerVisible;

        public GamePageViewModel()
        {
            // Prova a recuperare il salvataggio
            _gameManager = PreferenceUtilities.GetGame() ?? new GameManager();

            GuessCorrectCommand = new Command(OnCorrectPressed);
            GuessWrongCommand = new Command(OnWrongPressed);
            ToggleIDDrawerCommand = new Command(() => IsIDDrawerVisible = !IsIDDrawerVisible);
            ToggleVISADrawerCommand = new Command(() => IsVISADrawerVisible = !IsVISADrawerVisible);
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

        public bool IsIDDrawerVisible
        {
            get => _isIDDrawerVisible;
            set
            {
                _isIDDrawerVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsVISADrawerVisible
        {
            get => _isVISADrawerVisible;
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
            IsIDDrawerVisible = false;
            IsVISADrawerVisible = false;
        }


        private void OnCorrectPressed()
        {
            _gameManager.Guess(true);
            LoadNextPerson();
        }

        private void OnWrongPressed()
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
