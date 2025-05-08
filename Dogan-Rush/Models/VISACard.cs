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

        public DateOnly Birthdate
        {
            get => default;
            set
            {
            }
        }

        public string VISACode
        {
            get => default;
            set
            {
            }
        }

        public bool Sex
        {
            get => default;
            set
            {
            }
        }

        public DateOnly EmissionDate
        {
            get => default;
            set
            {
            }
        }

        public DateOnly ExpirationDate
        {
            get => default;
            set
            {
            }
        }

        public Countries Country
        {
            get => default;
            set
            {
            }
        }
    }
}