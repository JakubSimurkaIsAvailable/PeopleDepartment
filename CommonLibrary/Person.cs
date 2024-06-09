namespace CommonLibrary
{
    public class Person
    {
        public Person(string firstName, string lastName, string displayName, string? position, string email, List<string> department)
        {
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            Position = position;
            Email = email;
            Department = department;
            TitleAfter = DisplayName.Split(FirstName + " " + LastName).LastOrDefault();
            TitleBefore = DisplayName.Split(FirstName + " " + LastName).FirstOrDefault();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string? TitleBefore { get; private set; }
        public string? TitleAfter { get; private set; }
        public string? Position { get; set; }
        public string Email { get; set; }
        public List<string> Department { get; private set; }

        public string ToFormattedString()
        {
            return $"{DisplayName, -40} {Email}";
        }
    }
}
