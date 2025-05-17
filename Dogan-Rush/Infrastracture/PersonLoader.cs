using System.Text.Json;
using Dogan_Rush.Models;

namespace Dogan_Rush.Infrastracture
{
    public static class PersonLoader
    {
        private static readonly Random _random = new();

        public static List<PersonData>? LoadPeople(string? jsonPath = null)
        {
            if (string.IsNullOrEmpty(jsonPath))
            {
                jsonPath = Path.Combine("PersonData.json");

                if (!File.Exists(jsonPath))
                {
                    string fallbackPath = Path.Combine(AppContext.BaseDirectory, "Resources", "PersonData.json");
                    if (File.Exists(fallbackPath))
                    {
                        jsonPath = fallbackPath;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            try
            {
                string json = File.ReadAllText(jsonPath);
                return JsonSerializer.Deserialize<List<PersonData>>(json);
            }
            catch
            {
                return null;
            }
        }

        public static PersonData? GetRandomPerson(List<PersonData> people)
        {
            if (people == null || people.Count == 0) return null;
            int index = _random.Next(people.Count);
            return people[index];
        }
    }

}