namespace Dogan_Rush.Models
{
    public class Person
    {
        public string Name { get; }
        public string Surname { get; }
        public int Age { get; }
        public DateOnly BirthDate { get; }
        public IDCard IDCard { get; }
        public VISACard VISACard { get; }
        public string ImageData { get; }

        public Person(string name, string surname, int age, DateOnly birthDate, IDCard idCard, VISACard visaCard, string imageData)
        {
            Name = name;
            Surname = surname;
            Age = age;
            BirthDate = birthDate;
            IDCard = idCard;
            VISACard = visaCard;
            ImageData = imageData;
        }
    }
}