using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.Models;
using Dogan_Rush.Infrastracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dogan_Rush.View_Models
{
    public partial class GameViewModel:ObservableObject
    {

        [ObservableProperty]
        public GameManager? savedGame = null;


        [ObservableProperty]
        public int lifeCounter = 3;

        public GameViewModel()
        {
            // Constructor logic here
        }

        [RelayCommand]
        public void OnAppearing(GameManager game = null)
        {
            savedGame = null;

            var getLoadedGame = PreferenceUtilities.GetGame();

            if (getLoadedGame != null)
            {
                savedGame = getLoadedGame;
            }
            else if(game != null)
            {
                savedGame = game;
            }
            else
            {
                savedGame = new GameManager();
            }

        }

        [RelayCommand]
        public void OnDisappearing()
        {
            if (savedGame != null)
            {
                PreferenceUtilities.SaveGame(savedGame);
            }
        }

        [RelayCommand]
        public void OnNextTurn(bool guess)
        {
            savedGame.Guess(guess);
            LifeCounter = savedGame.LifesCounter;

            savedGame.NewTurn();

        }


    }
}
