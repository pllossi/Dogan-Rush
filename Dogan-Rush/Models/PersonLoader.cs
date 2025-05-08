using System.Text.Json;

namespace Dogan_Rush.Models
{
    public static class PersonLoader
    {
        private static readonly Random _random = new();

        public static List<PersonData> LoadPeople(string jsonPath)
        {
            string json = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<List<PersonData>>(json);
        }

        public static PersonData GetRandomPerson(List<PersonData> people)
        {
            if (people == null || people.Count == 0) return null;
            int index = _random.Next(people.Count);
            return people[index];
        }
    }
}