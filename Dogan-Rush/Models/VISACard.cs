namespace Dogan_Rush.Models
{
    public class VISACard
    {
        public VISACard(string name, string surname, DateOnly birthdate, string code, bool isMale, DateOnly emissionDate, DateOnly expirationDate, Countries country)
        {
            Name = name;
            Surname = surname;
            this.Birthdate = birthdate;
            VISACode = code;
            this.ExpirationDate = expirationDate;
            this.EmissionDate = emissionDate;
            Country = country;
            if (isMale)
            {
                SexDisplay = "Male";
            }
            else
            {
                SexDisplay = "Female";
            }
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string VISACode { get; set; }
        public DateOnly EmissionDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public bool Sex { get; set; }
        public Countries Country { get; set; }
        public string SexDisplay {  get; set; }
    }
}