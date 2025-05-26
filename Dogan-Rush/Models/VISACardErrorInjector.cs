using System;
using System.Collections.Generic;
using System.Linq;

namespace Dogan_Rush.Models
{
    public static class VISAErrorInjector
    {
        private static readonly Random rnd = new();

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

        public static void GenerateError(VISACard visa, int errorCount, DateOnly gameDate)
        {
            int difficulty = errorCount / 5;
            difficulty = Math.Clamp(difficulty, 1, 8);

            // Decide how many fields to corrupt (1 to 7)
            int fieldsToCorrupt = Math.Clamp(difficulty, 1, 7);

            var fields = new List<string> {
                nameof(visa.Birthdate),
                nameof(visa.EmissionDate),
                nameof(visa.ExpirationDate),
                nameof(visa.Name),
                nameof(visa.Surname),
                nameof(visa.VISACode),
                nameof(visa.Sex),
                nameof(visa.Country)
            };

            // Randomly pick which fields to corrupt (exclude sex sometimes to mimic your original logic)
            bool allowSexCorruption = difficulty < 3 || Chance(50);

            var corruptibleFields = allowSexCorruption ? fields : fields.Where(f => f != nameof(visa.Sex)).ToList();

            var chosenFields = corruptibleFields.OrderBy(_ => rnd.Next()).Take(fieldsToCorrupt).ToList();

            foreach (var field in chosenFields)
            {
                switch (field)
                {
                    case nameof(visa.Birthdate):
                        if (difficulty < 3)
                            visa.Birthdate = visa.Birthdate.AddYears(rnd.Next(-20, 21));
                        else
                            visa.Birthdate = visa.Birthdate.AddDays(rnd.Next(-10, 11));
                        break;

                    case nameof(visa.EmissionDate):
                        if (difficulty < 3)
                            visa.EmissionDate = visa.EmissionDate.AddYears(rnd.Next(-10, 11));
                        else
                            visa.EmissionDate = visa.EmissionDate.AddDays(rnd.Next(-30, 31));
                        break;

                    case nameof(visa.ExpirationDate):
                        if (difficulty < 4)
                            visa.ExpirationDate = visa.ExpirationDate.AddYears(rnd.Next(-10, 11));
                        else
                            visa.ExpirationDate = visa.ExpirationDate.AddDays(rnd.Next(-30, 31));
                        break;

                    case nameof(visa.Name):
                        if (difficulty < 4)
                            visa.Name = GetRandomEnumValue<PersonName>().ToString();
                        else
                            visa.Name = SlightlyCorruptString(visa.Name, difficulty);
                        break;

                    case nameof(visa.Surname):
                        if (difficulty < 4)
                            visa.Surname = GetRandomEnumValue<PersonSurname>().ToString();
                        else
                            visa.Surname = SlightlyCorruptString(visa.Surname, difficulty);
                        break;

                    case nameof(visa.VISACode):
                        visa.VISACode = SlightlyCorruptString(visa.VISACode, difficulty);
                        break;

                    case nameof(visa.Sex):
                        // Flip sex with some chance based on difficulty
                        if (Chance(40 + difficulty * 10))
                            visa.Sex = !visa.Sex;
                        break;

                    case nameof(visa.Country):
                        Array countries = Enum.GetValues(typeof(Countries));
                        Countries newCountry;
                        do
                        {
                            newCountry = (Countries)countries.GetValue(rnd.Next(countries.Length));
                        } while (newCountry == visa.Country);
                        visa.Country = newCountry;
                        break;
                }
            }
        }

        private static bool Chance(int percent) => rnd.Next(100) < percent;

        private static T GetRandomEnumValue<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rnd.Next(values.Length));
        }

        private static string SlightlyCorruptString(string input, int difficulty)
        {
            if (string.IsNullOrEmpty(input)) return input;

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
