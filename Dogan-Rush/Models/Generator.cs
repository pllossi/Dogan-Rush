using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Dogan_Rush.Models
{
    public class Generator
    {
        public Generator()
        {
            throw new System.NotImplementedException();
        }

        public PersonData Person
        {
            get => default;
            set
            {
            }
        }

        public Person GeneratedPerson
        {
            get => default;
            set
            {
            }
        }

        public IDCard GeneratedIDCard
        {
            get => default;
            set
            {
            }
        }

        public VISACard VISACard
        {
            get => default;
            set
            {
            }
        }

        public void generate()
        {
            var a = PersonLoader.LoadPeople("\"C:\\Users\\Cesare\\source\\repos\\Dogan-Rush\\Dogan-Rush\\Resources\\PersonData.json\"");
            var b = PersonLoader.GetRandomPerson(a);

            Random rnd = new Random();
            //GeneratedPerson = new Person();

        }
    }
}