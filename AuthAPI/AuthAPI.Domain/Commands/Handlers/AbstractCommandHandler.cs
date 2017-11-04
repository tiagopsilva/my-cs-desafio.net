using AuthAPI.Domain.ValueObjects;
using FluentValidation.Results;

namespace AuthAPI.Domain.Commands.Handlers
{
    public abstract class AbstractCommandHandler
    {
        protected MethodResult Success(object data = null)
        {
            if (data == null)
                return new MethodResult();
            return new MethodResult(data);
        }

        protected MethodResult Error(string errorMessage)
        {
            return new MethodResult(errorMessage);
        }

        protected MethodResult Error(ValidationResult validationResult)
        {
            return new MethodResult(validationResult.Errors);
        }
    }
}