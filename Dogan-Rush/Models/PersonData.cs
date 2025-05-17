namespace Dogan_Rush.Models
{
    public class PersonData
    {
        public PersonData(string imagename, int minage, int maxage, bool ismale)
        {
            imageName = imagename;
            minAge = minage;
            maxAge = maxage;
            isMale = ismale;
        }

        private static readonly Random _random = new();

        public string imageName { get; set; }
        public int minAge { get; set; }
        public int maxAge { get; set; }
        public bool isMale { get; set; }
    }
}