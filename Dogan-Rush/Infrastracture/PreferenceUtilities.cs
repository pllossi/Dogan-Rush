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
            string? json = Preferences.Get("game", null);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            Preferences.Remove("game");

            return JsonSerializer.Deserialize<GameManager>(json);
        }


    }
}
