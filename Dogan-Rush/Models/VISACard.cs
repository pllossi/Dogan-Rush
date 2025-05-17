namespace Dogan_Rush.Models
{
    public class VISACard
    {
        public VISACard(string name, string surname, DateOnly birthdate, string code, bool sex, DateOnly emissionDate, DateOnly expirationDate, Countries country)
        {
            Name = name;
            Surname = surname;
            this.Birthdate = birthdate;
            VISACode = code;
            this.ExpirationDate = expirationDate;
            this.EmissionDate = emissionDate;
            Country = country;
        }

        public string Name;

        public string Surname;

        public DateOnly Birthdate;

        public string VISACode;

        public bool Sex;

        public DateOnly EmissionDate;

        public DateOnly ExpirationDate;

        public Countries Country;
    }
}