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
        public Person? currPerson = null;

        private GameManager gameManager = new GameManager();

        [ObservableProperty]
        public int lifeCounter = 3;

        public GameViewModel()
        {
            // Constructor logic here
        }

        [RelayCommand]
        public void OnAppearing()
        {
            currPerson = null;

            var getLoadedPerson = PreferenceUtilities.GetPerson();

            if (getLoadedPerson != null)
            {
                currPerson = getLoadedPerson;
            }
            else
            {
                currPerson=gameManager.CurrentPerson;
                PreferenceUtilities.SavePerson(currPerson);
            }

        }

        [RelayCommand]
        public void OnDisappearing()
        {
            if (currPerson != null)
            {
                PreferenceUtilities.SavePerson(currPerson);
            }
        }

        [RelayCommand]
        public void OnNextTurn(bool guess)
        {
            gameManager.Guess(guess);
            LifeCounter = gameManager.LifesCounter;

            gameManager.NewTurn();

        }


    }
}
