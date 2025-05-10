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

        private static readonly JsonSerializerOptions _defaultJsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public static Person GetPerson()
        {
            throw new NotImplementedException();
        }

        public static void SavePerson(Person person)
        {
            throw new NotImplementedException();
        }


    }
}
