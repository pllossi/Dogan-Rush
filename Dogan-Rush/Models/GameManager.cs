namespace Dogan_Rush.Models
{
    public class GameManager
    {
        private int _turnCounter = 1;
        private int _lifeCounter = 3; //TODO: implement this in the game logic
        private DateOnly _gameDate;
        private Generator _gameGenerator;

        private GameStatus _gameStatus = GameStatus.Playing;

        private int nextValueForError = 0; //if the number from the random is lower than this the error will be placed.

        public GameManager()
        {
            DateOnly start = new DateOnly(1960, 1, 1);
            DateOnly end = new DateOnly(1980, 12, 31);

            int range = end.DayNumber - start.DayNumber;

            Random rand = new Random();
            int randomOffset = rand.Next(range + 1); // +1 to include endDate

            _gameDate = DateOnly.FromDayNumber(start.DayNumber + randomOffset);

            _gameGenerator = new Generator(_gameDate);

            CurrentPerson = _gameGenerator.generate();
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

        public int LifesCounter
        {
            get
            {
                return _lifeCounter;
            }
        }

        public GameStatus gameStatus
        {
            get { return _gameStatus; }
        }

        public void Guess(bool answer)
        {
            if (answer == isDocumentCorrect)
            {
                _turnCounter++;
                newTurn();
            }
            else
            {
                if (LifesCounter == 1)
                    _gameStatus = GameStatus.Lose;
                else
                {
                    _lifeCounter--;
                    newTurn();
                }
            }
        }

        public void newTurn()
        {
            CurrentPerson = _gameGenerator.generate();

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
            _turnCounter++;
        }
    }
}