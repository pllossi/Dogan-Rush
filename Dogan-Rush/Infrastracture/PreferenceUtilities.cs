using Dogan_Rush.Models;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json;

namespace Dogan_Rush.Infrastracture
{
    public static class PreferencesUtilities
    {
        

        public static void SaveGame(GameManager game)
        {
                Preferences.Remove("game");
                string json = JsonConvert.SerializeObject(game);
                Preferences.Set("game", json);
        }

        public static GameManager? GetGame()
        {
            string? json = Preferences.Get("game", null);
            if (string.IsNullOrEmpty(json))
                return null;

            var game = JsonConvert.DeserializeObject<GameManager>(json);
            if (game != null && game.GameGenerator == null)
                game.GameGenerator = new Generator(game.GameDate);

            return game;
        }

        public static void ClearGame()
        {
            Preferences.Remove("game");
        }

    }
}