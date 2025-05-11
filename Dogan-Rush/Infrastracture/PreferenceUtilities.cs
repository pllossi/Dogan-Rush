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

        public static void SavePerson(Person person)
        {
            string json = JsonSerializer.Serialize(person);
            Preferences.Set("person", json);
        }

        public static Person? GetPerson()
        {
            string? json = Preferences.Get("person", null);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            // Clear the stored person data after loading
            Preferences.Remove("person");

            return JsonSerializer.Deserialize<Person>(json);
        }


    }
}
