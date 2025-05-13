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

        public DateOnly EmissionDate
        {
            get => default;
            set
            {
            }
        }

        public DateOnly ExpiringDate
        {
            get => default;
            set
            {
            }
        }

        public string IDcode
        {
            get => default;
            set
            {
            }
        }

        public DateOnly BirthDate
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

        public Countries Nationality
        {
            get => default;
            set
            {
            }
        }

        public Countries Countries
        {
            get => default;
            set
            {
            }
        }
    }
    }