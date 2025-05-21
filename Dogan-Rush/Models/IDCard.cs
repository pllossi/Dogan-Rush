namespace Dogan_Rush.Models
{
    public class IDCard
    {
        public IDCard(string name, string surname, DateOnly birthdate, string code, bool sex, DateOnly emissionDate, DateOnly expirationDate,Countries nationality)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthdate;
            IDcode = code;
            ExpiringDate = expirationDate;
            EmissionDate = emissionDate;
            Sex = sex;
            Nationality = nationality;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string IDcode { get; set; }
        public DateOnly EmissionDate { get; set; }
        public DateOnly ExpiringDate { get; set; }
        public bool Sex { get; set; }
        public Countries Nationality { get; set; }
        public string SexDisplay => Sex ? "Male" : "Female";
    }
}