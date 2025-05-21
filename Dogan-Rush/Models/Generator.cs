using Dogan_Rush.Infrastracture;
using System.Text;

namespace Dogan_Rush.Models
{
    public class Generator
    {
        private DateOnly _gameDate;
        private List<PersonData> a;

        public Generator(DateOnly gameDate)
        {
            _gameDate = gameDate;
            a = PersonLoader.LoadPeopleData();
        }

        public Person generate()
        {
            PersonName currname;
            PersonData tookData = PersonLoader.GetRandomPerson(a);

            Random rnd = new Random();
            string imageData = tookData.imageName;
            int age = rnd.Next(tookData.minAge, tookData.maxAge);
            if(tookData.isMale == true)
                currname = (PersonName)rnd.Next(0, 164);
            else
                currname = (PersonName)rnd.Next(163, 350);
            string name = currname.ToString();
            PersonSurname currsurname = (PersonSurname)rnd.Next(0, 188);
            string surname = currsurname.ToString();
            bool isMale = tookData.isMale;
            DateOnly birthDate = GetRandomDateOnly(age, _gameDate);

            DateOnly emissionDateVisa = GenerateRandomEmissionDate(_gameDate);
            DateOnly expirationDateVisa = emissionDateVisa.AddYears(5);

            DateOnly emissionDateID = GenerateRandomEmissionDate(_gameDate);
            DateOnly expirationDateID = emissionDateID.AddYears(5);

            Countries country = (Countries)rnd.Next(0, 193);
            string code = GenerateRandomCode();

            VISACard visa = new VISACard(name, surname, birthDate, code, isMale, emissionDateVisa, expirationDateVisa, country);
            IDCard id = new IDCard(name, surname, birthDate, code, isMale, emissionDateID, expirationDateID,country);

            return new Person(name, surname, age, birthDate, id, visa, imageData);
        }

        private DateOnly GetRandomDateOnly(int age, DateOnly currentDate)
        {
            // Calculate the earliest and latest possible birthdates
            DateOnly startDate = currentDate.AddYears(-age - 1).AddDays(1); // After turning (age+1)
            DateOnly endDate = currentDate.AddYears(-age); // Before turning age

            // Get range of days between start and end date
            int range = endDate.DayNumber - startDate.DayNumber;

            // Generate random number of days to add to the start date
            Random rand = new Random();
            int randomDays = rand.Next(range + 1); // +1 to include endDate

            return DateOnly.FromDayNumber(startDate.DayNumber + randomDays);
        }

        private DateOnly GetRandomDateOnly(DateOnly startDate, DateOnly endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date must be earlier than or equal to end date.");

            int range = endDate.DayNumber - startDate.DayNumber;
            Random rand = new Random();
            int randomOffset = rand.Next(range + 1); // +1 to include endDate

            return DateOnly.FromDayNumber(startDate.DayNumber + randomOffset);
        }

        private DateOnly GenerateRandomEmissionDate(DateOnly currentDate)
        {
            // Calculate the range (from today to 5 years ago)
            DateOnly fiveYearsAgo = currentDate.AddYears(-5);

            // Generate a random emission date between today and five years ago
            DateOnly emissionDate = GetRandomDateOnly(fiveYearsAgo, currentDate);

            return emissionDate;
        }

        public string GenerateRandomCode()
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder code = new StringBuilder(9);

            Random rand = new Random();

            for (int i = 0; i < 9; i++)
            {
                // Pick a random character from validChars
                char randomChar = validChars[rand.Next(validChars.Length)];
                code.Append(randomChar);
            }

            return code.ToString();
        }
    }
}