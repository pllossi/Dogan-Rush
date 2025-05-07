using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dogan_Rush.Models
{
    public class GameManager
    {
        private DateOnly _gameDate;
        private Generator _gameGenerator;

        public GameManager()
        {


            DateOnly start = new DateOnly(1960, 1, 1);
            DateOnly end = new DateOnly(1980, 12, 31);

            int range = end.DayNumber - start.DayNumber;

            Random rand = new Random();
            int randomOffset = rand.Next(range + 1); // +1 to include endDate

            _gameDate = DateOnly.FromDayNumber(start.DayNumber + randomOffset);

            _gameGenerator = new Generator(_gameDate);

        }

        public bool isDocumentCorrect
        {
            get => default;
            set
            {
            }
        }

        public Person CurrentPerson
        {
            get => default;
            set
            {
            }
        }

        public Generator Generator
        {
            get => default;
            set
            {
            }
        }

        public void newPerson()
        {
            throw new System.NotImplementedException();
        }

        public void newTurn()
        {
            throw new System.NotImplementedException();
        }




    }
}