namespace Dogan_Rush.Models
{
    public class IDCard
    {
        public IDCard(string name, string surname, DateOnly birthdate, string code, bool sex, DateOnly emissionDate, DateOnly expirationDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthdate;
            IDcode = code;
            ExpiringDate = expirationDate;
            EmissionDate = emissionDate;
            Sex = sex;
        }

        public string Name;
        public string Surname;

        public DateOnly EmissionDate;
        public DateOnly ExpiringDate;

        public string IDcode;

        public DateOnly BirthDate;

        public bool Sex;

        public Countries Nationality;

        public Countries Countries;
    }
}