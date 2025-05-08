namespace Dogan_Rush.Models
{
    public class Person
    {
        public Person(string name, string surname, int age, DateOnly birthday, IDCard idCard, VISACard visaCard, string imageData)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Birthday = birthday;
            IDCard = idCard;
            VISACard = visaCard;
            ImageData = imageData;
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

        public VISACard VISACard
        {
            get => default;
            set
            {
            }
        }

        public string ImageData
        {
            get => default;
            set
            {
            }
        }
    }
}