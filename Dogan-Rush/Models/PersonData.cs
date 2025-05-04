using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dogan_Rush.Models
{
    public class PersonData
    {
        private static readonly Random _random = new();

        public string ImageName { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsMale { get; set; }

        public int Age => _random.Next(MinAge, MaxAge + 1);
        public string Gender => IsMale ? "Male" : "Female";

        public override string ToString()
        {
            return $"[{ImageName}] Age: {Age}, Gender: {Gender}";
        }
    }


}
}
