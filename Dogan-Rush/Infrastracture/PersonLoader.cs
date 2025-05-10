using System.Text.Json;
using Dogan_Rush.Models;

namespace Dogan_Rush.Infrastracture
{
    public static class PersonLoader
    {
        private static readonly Random _random = new();

        public static List<PersonData> LoadPeople(string jsonPath= "cd..\\Resources\\PersonData.json")
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