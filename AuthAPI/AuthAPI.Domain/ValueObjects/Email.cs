namespace AuthAPI.Domain.ValueObjects
{
    public class Email
    {
        public Email(string email)
        {
            Value = email;
        }

        public string Value { get; }
    }
}