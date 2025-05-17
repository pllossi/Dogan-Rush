namespace Dogan_Rush.Models
{
    public static class VISAErrorInjector
    {
        private static readonly Dictionary<char, char[]> similarLetters = new()
        {
            { 'b', new[] { 'd', 'h' } }, { 'd', new[] { 'b', 'o' } },
            { 'm', new[] { 'n' } }, { 'n', new[] { 'm', 'r' } },
            { 'i', new[] { 'l', 'j' } }, { 'l', new[] { 'i', 't' } },
            { 'o', new[] { '0', 'd' } }, { '0', new[] { 'o' } },
            { 'e', new[] { 'c' } }, { 's', new[] { 'z' } },
            { 'z', new[] { 's' } }, { 'a', new[] { 'o' } },
            { 'u', new[] { 'v' } }, { 'v', new[] { 'u' } }
        };

        public static void GenerateError(VISACard visa, int errorCount)
        {
            int difficulty = errorCount / 5;
            errorCount++;

            Random rnd = new Random();

            // Lower chance of sex change at higher difficulty
            bool forceSexChange = difficulty < 2 || rnd.Next(0, difficulty + 3) == 0;

            int[] possibleErrors = forceSexChange
                ? Enumerable.Range(0, 8).ToArray()
                : Enumerable.Range(0, 7).ToArray(); // skip index 6 (sex)

            int errorPos = possibleErrors[rnd.Next(possibleErrors.Length)];

            switch (errorPos)
            {
                case 0: // Birthdate
                    visa.Birthdate = difficulty < 2
                        ? visa.Birthdate.AddYears(rnd.Next(-5, 6))
                        : visa.Birthdate.AddDays(rnd.Next(-1, 2));
                    break;

                case 1: // EmissionDate
                    visa.EmissionDate = difficulty < 2
                        ? visa.EmissionDate.AddYears(rnd.Next(-3, 4))
                        : visa.EmissionDate.AddDays(rnd.Next(-2, 3));
                    break;

                case 2: // ExpirationDate
                    visa.ExpirationDate = difficulty < 2
                        ? visa.ExpirationDate.AddYears(rnd.Next(-3, 4))
                        : visa.ExpirationDate.AddDays(rnd.Next(-2, 3));
                    break;

                case 3: // Name
                    visa.Name = difficulty < 2
                        ? GetRandomEnumValue<PersonName>().ToString()
                        : SlightlyCorruptString(visa.Name, difficulty);
                    break;

                case 4: // Code
                    visa.VISACode = SlightlyCorruptString(visa.VISACode, difficulty);
                    break;

                case 5: // Country
                    Array values = Enum.GetValues(typeof(Countries));
                    Countries newCountry;
                    do
                    {
                        newCountry = (Countries)values.GetValue(rnd.Next(values.Length));
                    } while (newCountry == visa.Country);
                    visa.Country = newCountry;
                    break;

                case 6: // Sex
                    visa.Sex = !visa.Sex;
                    break;

                default: // Surname
                    visa.Surname = difficulty < 2
                        ? GetRandomEnumValue<PersonSurname>().ToString()
                        : SlightlyCorruptString(visa.Surname, difficulty);
                    break;
            }
        }

        private static T GetRandomEnumValue<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            Random rnd = new Random();
            return (T)values.GetValue(rnd.Next(values.Length));
        }

        private static string SlightlyCorruptString(string input, int difficulty)
        {
            if (string.IsNullOrEmpty(input)) return input;

            Random rnd = new Random();
            char[] chars = input.ToCharArray();
            int changes = Math.Clamp(difficulty + 1, 1, chars.Length);

            for (int i = 0; i < changes; i++)
            {
                int index = rnd.Next(chars.Length);
                char original = char.ToLower(chars[index]);

                if (similarLetters.ContainsKey(original))
                {
                    char[] options = similarLetters[original];
                    chars[index] = options[rnd.Next(options.Length)];
                }
                else
                {
                    chars[index] = (char)rnd.Next(65, 91); // Random uppercase A-Z
                }
            }

            return new string(chars);
        }
    }
}