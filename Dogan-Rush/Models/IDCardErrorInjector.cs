namespace Dogan_Rush.Models
{
    public static class IDCardErrorInjector
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

        public static void GenerateError(IDCard id, int errorCount)
        {
            int difficulty = errorCount / 5;
            errorCount++;

            Random rnd = new Random();

            // Lower chance of flipping sex as difficulty increases
            bool forceSexChange = difficulty < 2 || rnd.Next(0, difficulty + 3) == 0;

            int[] possibleErrors = forceSexChange
                ? Enumerable.Range(0, 8).ToArray()
                : Enumerable.Range(0, 7).ToArray(); // skip index 6 (sex)

            int errorPos = possibleErrors[rnd.Next(possibleErrors.Length)];

            switch (errorPos)
            {
                case 0: // BirthDate
                    id.BirthDate = difficulty < 2
                        ? id.BirthDate.AddYears(rnd.Next(-5, 6))
                        : id.BirthDate.AddDays(rnd.Next(-1, 2));
                    break;

                case 1: // EmissionDate
                    id.EmissionDate = difficulty < 2
                        ? id.EmissionDate.AddYears(rnd.Next(-3, 4))
                        : id.EmissionDate.AddDays(rnd.Next(-2, 3));
                    break;

                case 2: // ExpiringDate
                    id.ExpiringDate = difficulty < 2
                        ? id.ExpiringDate.AddYears(rnd.Next(-3, 4))
                        : id.ExpiringDate.AddDays(rnd.Next(-2, 3));
                    break;

                case 3: // Name
                    id.Name = difficulty < 2
                        ? GetRandomEnumValue<PersonName>().ToString()
                        : SlightlyCorruptString(id.Name, difficulty);
                    break;

                case 4: // IDcode
                    id.IDcode = SlightlyCorruptString(id.IDcode, difficulty);
                    break;

                case 5: // Nationality
                    Array countries = Enum.GetValues(typeof(Countries));
                    Countries newCountry;
                    do
                    {
                        newCountry = (Countries)countries.GetValue(rnd.Next(countries.Length));
                    } while (newCountry == id.Nationality);
                    id.Nationality = newCountry;
                    break;

                case 6: // Sex (if allowed at current difficulty)
                    id.Sex = !id.Sex;
                    break;

                default: // Surname
                    id.Surname = difficulty < 2
                        ? GetRandomEnumValue<PersonSurname>().ToString()
                        : SlightlyCorruptString(id.Surname, difficulty);
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
                    chars[index] = (char)rnd.Next(65, 91); // Random A-Z
                }
            }

            return new string(chars);
        }
    }
}