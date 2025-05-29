namespace Dogan_Rush.Models
{
    public class IDCard
    {
        public IDCard(string name, string surname, DateOnly birthdate, string code, bool isMale, DateOnly emissionDate, DateOnly expirationDate, Countries nationality)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthdate;
            IDcode = code;
            ExpiringDate = expirationDate;
            EmissionDate = emissionDate;
            Sex = isMale;
            Nationality = nationality;
            if (isMale)
            {
                SexDisplay = "Male";
            }else
            {
                SexDisplay = "Female";
            }
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string IDcode { get; set; }
        public DateOnly EmissionDate { get; set; }
        public DateOnly ExpiringDate { get; set; }
        public bool Sex { get; set; }
        public Countries Nationality { get; set; }
        public string SexDisplay {  get; set; }
    }
}