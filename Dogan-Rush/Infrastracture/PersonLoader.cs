using Dogan_Rush.Models;
using System.Text.Json;

namespace Dogan_Rush.Infrastracture
{
    public static class PersonLoader
    {
        private static readonly Random _random = new();
        private static List<PersonData> nullPerson = new List<PersonData>(1) { new PersonData("person01.png", 20, 30, true) };

        public static List<PersonData>? LoadPeopleData()
        {
            try
            {
                string jsonPath = FindJsonFile("PersonData.json");
                if (jsonPath == null) return nullPerson;

                string json = File.ReadAllText(jsonPath);
                return JsonSerializer.Deserialize<List<PersonData>>(json);
            }
            catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
            {
                Console.WriteLine($"Errore: {ex.GetType().Name} - {ex.Message}");
                return null;
            }
        }

        private static string? FindJsonFile(string fileName)
        {
            // 1. Cerca nella directory corrente + "Resources"
            string currentDir = Directory.GetCurrentDirectory();
            string jsonPath = Path.Combine(currentDir, "Resources", fileName);
            if (File.Exists(jsonPath)) return jsonPath;

            // 2. Cerca nella directory dell'eseguibile + "Resources"
            string exeDir = AppContext.BaseDirectory;
            jsonPath = Path.Combine(exeDir, "Resources", fileName);
            if (File.Exists(jsonPath)) return jsonPath;

            // 3. Ricerca ricorsiva LIMITATA (evita cartelle di sistema)
            try
            {
                var files = SafeSearch(currentDir, fileName, maxDepth: 3);
                return files.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        private static List<string> SafeSearch(string rootDir, string fileName, int maxDepth)
        {
            var results = new List<string>();
            var dirsToSearch = new Queue<(string path, int depth)>();
            dirsToSearch.Enqueue((rootDir, 0));

            while (dirsToSearch.Count > 0)
            {
                var (currentDir, depth) = dirsToSearch.Dequeue();
                if (depth > maxDepth) continue;

                try
                {
                    // Cerca file
                    string[]? files = Directory.GetFiles(currentDir, fileName);
                    results.AddRange(files);

                    // Aggiunge sottocartelle alla coda
                    foreach (var dir in Directory.GetDirectories(currentDir))
                    {
                        dirsToSearch.Enqueue((dir, depth + 1));
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // Salta cartelle senza permessi
                    continue;
                }
            }

            return results;
        }

        public static PersonData? GetRandomPerson(List<PersonData> people)
        {
            if (people == null || people.Count == 0) return null;
            int index = _random.Next(people.Count);
            return people[index];
        }
    }
}