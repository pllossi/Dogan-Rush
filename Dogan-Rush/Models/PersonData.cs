namespace Dogan_Rush.Models
{
    public class PersonData
    {
        private static readonly Random _random = new();

        public string imageName { get; set; }
        public int minAge { get; set; }
        public int maxAge { get; set; }
        public bool isMale { get; set; }
    }
}