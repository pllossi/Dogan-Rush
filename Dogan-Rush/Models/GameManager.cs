namespace Dogan_Rush.Models
{
    public class GameManager
    {
        public int TurnCounter { get; set; } = 0;
        public int ErrorsCounter { get; set; } = 0;
        public DateOnly GameDate { get; set; }
        public Generator GameGenerator { get; set; }
        public GameStatus GameStatus { get; set; } = GameStatus.Playing;
        public int NextValueForError { get; set; } = 50;

        public bool IsDocumentCorrect { get; set; }
        public Person CurrentPerson { get; set; }

        public GameManager()
        {
            DateOnly start = new DateOnly(1960, 1, 1);
            DateOnly end = new DateOnly(1980, 12, 31);

            int range = end.DayNumber - start.DayNumber;

            Random rand = new Random();
            int randomOffset = rand.Next(range + 1); // +1 to include endDate

            GameDate = DateOnly.FromDayNumber(start.DayNumber + randomOffset);
            GameGenerator = new Generator(GameDate);
        }

        public void Guess(bool answer)
        {
            if (answer == IsDocumentCorrect)
                NewTurn();
            else
            {
                ErrorsCounter++;
                NewTurn();
            }
        }

        public void NewTurn()
        {
            CurrentPerson = GameGenerator.generate();
            if (TurnCounter > 1)
            {
                Random rnd = new Random();
                int randomValue = rnd.Next(0, 100);

                if (randomValue < NextValueForError)
                {
                    int document = rnd.Next(0, 2);
                    IsDocumentCorrect = false;

                    if (document == 0)
                        IDCardErrorInjector.GenerateError(CurrentPerson.IDCard, TurnCounter);
                    else
                        VISAErrorInjector.GenerateError(CurrentPerson.VISACard, TurnCounter);
                }
                else
                {
                    IsDocumentCorrect = true;
                }

                NextValueForError = rnd.Next(0, 100);
            }
            TurnCounter++;
        }
    }
}