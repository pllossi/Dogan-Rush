namespace Dogan_Rush.Models
{
    public class GameManager
    {
        private int _turnCounter = 0; //the game starts from turn 0
        private int _errorCounter = 0; //TODO: implement this in the game logic
        private DateOnly _gameDate;
        private Generator _gameGenerator;

        private GameStatus _gameStatus = GameStatus.Playing;

        private int nextValueForError = 50; //if the number from the random is lower than this the error will be placed.

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
            get;
            private set;
        }

        public Person CurrentPerson
        {
            get;
            private set;
        }

        public int TurnCounter
        {
            get
            {
                return _turnCounter;
            }
        }

        public int ErrorsCounter
        {
            get
            {
                return _errorCounter;
            }
        }

        public DateOnly GameDate
        {
            get
            {
                return _gameDate;
            }
        }

        public void Guess(bool answer)
        {
            if (answer == isDocumentCorrect)
            {
                NewTurn();
            }
            else
            {
                
                _errorCounter++;
                NewTurn();
                
            }
        }

        public void NewTurn()
        {
            CurrentPerson = _gameGenerator.generate();
            if (TurnCounter > 1)
            {
                Random rnd = new Random();

                int randomValue = rnd.Next(0, 100);

                if (randomValue < nextValueForError)
                {
                    int document = rnd.Next(0, 2); // 0 = IDCard, 1 = VisaCard
                    isDocumentCorrect = false;

                    if (document == 0)
                    {
                        IDCardErrorInjector.GenerateError(CurrentPerson.IDCard, _turnCounter);
                    }
                    else
                    {
                        VISAErrorInjector.GenerateError(CurrentPerson.VISACard, _turnCounter);
                    }
                }
                else
                {
                    isDocumentCorrect = true;
                }

                nextValueForError = rnd.Next(0, 100);
            }
            _turnCounter++;
        }
    }
}