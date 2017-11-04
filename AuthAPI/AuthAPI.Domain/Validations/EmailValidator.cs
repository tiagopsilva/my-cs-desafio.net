using AuthAPI.Domain.ValueObjects;
using FluentValidation;

namespace AuthAPI.Domain.Validations
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Value)
                .Length(5, 255)
                .EmailAddress();
        }
    }
}