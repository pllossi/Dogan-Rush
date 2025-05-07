using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dogan_Rush.Models
{
    public class Person
    {
        public Person(string name, string surname, int age, DateOnly birthday, IDCard idCard=null, VISACard visaCard =null, string imageData)
        {
            throw new System.NotImplementedException();
        }

        public string Name
        {
            get => default;
            set
            {
            }
        }

        public string Surname
        {
            get => default;
            set
            {
            }
        }

        public int Age
        {
            get => default;
            set
            {
            }
        }

        public DateOnly Birthday
        {
            get => default;
            set
            {
            }
        }

        public int Sex
        {
            get => default;
            set
            {
            }
        }

        public IDCard IDCard
        {
            get => default;
            set
            {
            }
        }

        public VISACard GeneratedVISACard
        {
            get => default;
            set
            {
            }
        }
    }
}