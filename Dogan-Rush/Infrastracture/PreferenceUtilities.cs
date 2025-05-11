using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dogan_Rush.Models;

namespace Dogan_Rush.Infrastracture
{
    public static class PreferenceUtilities
    {

        public static void SaveGame(GameManager game)
        {
            string json = JsonSerializer.Serialize(game);
            Preferences.Set("game", json);
        }

        public static GameManager? GetGame()
        {
            string? json = Preferences.Get("person", null);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            // Clear the stored person data after loading
            Preferences.Remove("game");

            return JsonSerializer.Deserialize<GameManager>(json);
        }


    }
}
