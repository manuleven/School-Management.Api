namespace SchoolManagement.Domain.ValueObjects
{
    public class FullName
    {
        public string FirstName { get;private set; }

        public string LastName { get; private set; }

        private FullName() { }

        private FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException("First name is required.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException("last name is required.", nameof(lastName));

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }

        public static FullName Create(string firstName, string lastName)
        {
            return new FullName(firstName, lastName);
        }

        public string GetFullName() => $"{FirstName} {LastName}";


    }

}
