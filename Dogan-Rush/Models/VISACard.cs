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

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string VISACode { get; set; }
        public DateOnly EmissionDate { get; set; }
        public  DateOnly ExpirationDate { get; set; }
        public bool Sex { get; set; }
        public Countries Country { get; set; }
        public string SexDisplay => Sex ? "Male" : "Female";
    }
}