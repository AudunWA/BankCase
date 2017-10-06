namespace BankCase
{
    /// <summary>
    /// Represents a bank customer
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Helper variable for generating account names
        /// </summary>
        private int accountIDCounter = 1;

        /// <summary>
        /// Creates a new person
        /// </summary>
        /// <param name="firstName">The first name of the person</param>
        /// <param name="lastName">The last name of the person</param>
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// The first name of the person
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// The last name of the person
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// The fund that is currently available to the person
        /// </summary>
        public Money AvailableFunds { get; set; }


        /// <summary>
        /// Generates a new account name based on the person's name and a counter number
        /// </summary>
        /// <returns>The new account name</returns>
        public string GetNextAccountName()
        {
            return $"{FirstName} {LastName} {accountIDCounter++}";
        }
    }
}