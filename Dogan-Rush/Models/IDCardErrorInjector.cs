using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Dogan_Rush.Models
{
    public static class IDCardErrorInjector
    {
        private static readonly Random rnd = new();

        // Map some common place names / issuing authorities for typo simulation
        private static readonly string[] CommonAuthorities = new[]
        {
            "Department of State", "Ministry of Internal Affairs", "City Hall",
            "Immigration Office", "Police Department", "Registry Office"
        };

        // Generate full card error based on errorCounter (difficulty) and gamedate
        public static void GenerateError(IDCard id, int errorCounter, DateOnly gameDate)
        {
            int difficulty = errorCounter;

            // Decide how many fields to corrupt based on difficulty (min 1, max all 8 fields)
            int fieldsToCorrupt = Math.Clamp(difficulty, 1, 8);

            // List of field names to corrupt
            var fields = new List<string> {
                nameof(id.BirthDate),
                nameof(id.EmissionDate),
                nameof(id.ExpiringDate),
                nameof(id.Name),
                nameof(id.Surname),
                nameof(id.IDcode),
                nameof(id.Sex),
                nameof(id.Nationality)
            };

            // Randomly pick which fields to corrupt this run
            var chosenFields = fields.OrderBy(_ => rnd.Next()).Take(fieldsToCorrupt).ToList();

            foreach (var field in chosenFields)
            {
                switch (field)
                {
                    case nameof(id.BirthDate):
                        // Corrupt BirthDate: bigger difficulty = larger changes (up to ±30 years)
                        int yearOffset = rnd.Next(-30 * difficulty, 31 * difficulty);
                        int dayOffset = rnd.Next(-60, 61);
                        id.BirthDate = id.BirthDate.AddYears(yearOffset).AddDays(dayOffset);
                        break;

                    case nameof(id.EmissionDate):
                    case nameof(id.ExpiringDate):
                        // Corrupt dates but keep their relation (5 years apart approx)
                        var (newEmission, newExpiration) = CorruptDates(id.EmissionDate, id.ExpiringDate, difficulty, gameDate);
                        id.EmissionDate = newEmission;
                        id.ExpiringDate = newExpiration;
                        break;

                    case nameof(id.Name):
                        // Sometimes full random name, sometimes typoed
                        if (difficulty < 3 && Chance(40))
                        {
                            id.Name = GetRandomEnumValue<PersonName>().ToString();
                        }
                        else
                        {
                            id.Name = CorruptString(id.Name, difficulty);
                        }
                        break;

                    case nameof(id.Surname):
                        if (difficulty < 3 && Chance(40))
                        {
                            id.Surname = GetRandomEnumValue<PersonSurname>().ToString();
                        }
                        else
                        {
                            id.Surname = CorruptString(id.Surname, difficulty);
                        }
                        break;

                    case nameof(id.IDcode):
                        // IDs often corrupted with homoglyphs & typos
                        id.IDcode = CorruptString(id.IDcode, difficulty);
                        break;

                    case nameof(id.Sex):
                        // Flip sex only if difficulty allows and random chance
                        if (difficulty >= 2 && Chance(50))
                        {
                            id.Sex = !id.Sex;
                        }
                        break;

                    case nameof(id.Nationality):
                        if (difficulty >= 1)
                        {
                            Array countries = Enum.GetValues(typeof(Countries));
                            Countries newCountry;
                            do
                            {
                                newCountry = (Countries)countries.GetValue(rnd.Next(countries.Length));
                            } while (newCountry == id.Nationality);
                            id.Nationality = newCountry;
                        }
                        break;
                }
            }

            // Optionally corrupt issuing authority with typo (simulate bad typing)
            if (difficulty >= 2 && Chance(40))
            {
                string authority = CommonAuthorities[rnd.Next(CommonAuthorities.Length)];
                authority = CorruptString(authority, difficulty);
                // You can add an IssuingAuthority property to IDCard and set here
                // id.IssuingAuthority = authority;
            }
        }

        // -------------------------
        // DATE CORRUPTION
        // -------------------------
        private static (DateOnly emission, DateOnly expiration) CorruptDates(
            DateOnly originalEmission,
            DateOnly originalExpiration,
            int errorLevel,
            DateOnly gameDate)
        {
            DateOnly emission = originalEmission;
            DateOnly expiration = originalExpiration;

            int corruptionType = rnd.Next(0, Math.Clamp(errorLevel + 1, 1, 7));

            switch (corruptionType)
            {
                case 0:
                    // Slight day offset to expiration
                    expiration = expiration.AddDays(rnd.Next(-60, 60));
                    break;

                case 1:
                    // Slight month shift to expiration
                    expiration = expiration.AddMonths(rnd.Next(-30, 30));
                    break;

                case 2:
                    // Expiration before emission (illegal)
                    expiration = emission.AddDays(-rnd.Next(1, 30));
                    break;

                case 3:
                    // Future emission date (after game date)
                    emission = gameDate.AddDays(rnd.Next(1, 180));
                    expiration = emission.AddYears(5);
                    break;

                case 4:
                    // Expiration too far (over 5 years)
                    expiration = emission.AddYears(rnd.Next(1,8)).AddDays(rnd.Next(1, 170));
                    break;

                case 5:
                    // Emission pushed back years, expiration adjusted accordingly
                    emission = emission.AddYears(-rnd.Next(1, 8));
                    expiration = emission.AddYears(5);
                    break;

                case 6:
                    // Birth date shifted as well (simulate realistic age error)
                    emission = emission.AddDays(rnd.Next(-10, 11));
                    expiration = expiration.AddDays(rnd.Next(-10, 11));
                    break;
            }

            return (emission, expiration);
        }

        // -------------------------
        // STRING CORRUPTION
        // -------------------------
        private static string CorruptString(string input, int errorLevel)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            string result = input;

            if (errorLevel > 0 && Chance(30))
                result = RemoveDiacritics(result);

            if (errorLevel > 1 && Chance(40))
                result = ApplyHomoglyphs(result);

            if (errorLevel > 2 && Chance(30))
                result = RandomCaseFlip(result);

            if (errorLevel > 3 && Chance(30))
                result = IntroduceTypo(result);

            if (errorLevel > 4 && Chance(20))
                result = DuplicateChar(result);

            return result;
        }

        // -------------------------
        // HELPERS
        // -------------------------
        private static bool Chance(int percent) => rnd.Next(0, 100) < percent;

        private static string RemoveDiacritics(string input)
        {
            var normalized = input.Normalize(NormalizationForm.FormD);
            return new string(normalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray())
                .Normalize(NormalizationForm.FormC);
        }

        private static string ApplyHomoglyphs(string input)
        {
            Dictionary<string, string> homoglyphs = new()
            {
                { "rn", "m" }, { "cl", "d" }, { "O", "0" }, { "o", "0" },
                { "l", "1" }, { "I", "1" }, { "e", "3" }, { "a", "@" },
                { "s", "$" }, { "B", "8" }, { "Z", "2" }
            };

            string result = input;

            foreach (var pair in homoglyphs.OrderByDescending(p => p.Key.Length))
            {
                if (Chance(25))
                    result = result.Replace(pair.Key, pair.Value);
            }

            return result;
        }

        private static string RandomCaseFlip(string input)
        {
            StringBuilder sb = new();
            foreach (char c in input)
            {
                sb.Append(Chance(50) ? char.ToUpper(c) : char.ToLower(c));
            }
            return sb.ToString();
        }

        private static string IntroduceTypo(string input)
        {
            if (input.Length < 2) return input;
            int index = rnd.Next(0, input.Length - 1);

            char[] chars = input.ToCharArray();
            (chars[index], chars[index + 1]) = (chars[index + 1], chars[index]);

            return new string(chars);
        }

        private static string DuplicateChar(string input)
        {
            if (input.Length < 1) return input;
            int index = rnd.Next(0, input.Length);
            return input.Insert(index, input[index].ToString());
        }

        private static T GetRandomEnumValue<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rnd.Next(values.Length));
        }
    }
}
