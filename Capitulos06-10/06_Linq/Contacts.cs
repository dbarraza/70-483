namespace _06_Linq
{
    public class Contact
    {
        public int ContactId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Contact(int contactId, string firstName, string lastName)
        {
            ContactId = contactId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
