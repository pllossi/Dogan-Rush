using Dogan_Rush.Models;
using System.Text.Json;

namespace Dogan_Rush.Infrastracture
{
    public static class PersonLoader
    {
        private static readonly Random _random = new();
        private static List<PersonData> nullPerson = new List<PersonData>(1) { new PersonData("person01.png", 20, 30, true) };

        public static List<PersonData> LoadPeopleData()
        {
            try
            {
                using var stream = FileSystem.OpenAppPackageFileAsync("PersonData.json").Result;
                using var reader = new StreamReader(stream);
                string json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<List<PersonData>>(json) ?? nullPerson;
            }
            catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
            {
                return nullPerson;
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