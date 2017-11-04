using System.Collections.Generic;
using System.Linq;
using System.Net;
using AuthAPI.Domain.Extensions;
using FluentValidation.Results;

namespace AuthAPI.Domain.ValueObjects
{
    public class MethodResult
    {
        private readonly List<ValidationFailure> _failures = new List<ValidationFailure>();

        public MethodResult()
        {
            ErrorCode = HttpStatusCode.BadRequest;
        }

        public MethodResult(object data)
            : this()
        {
            Data = data;
        }

        public MethodResult(string errorMessage)
            : this()
        {
            AddFailure(errorMessage);
        }

        public MethodResult(string propertyName, string errorMessage)
            : this()
        {
            AddFailure(propertyName, errorMessage);
        }

        public MethodResult(ValidationFailure validationFailure)
            : this()
        {
            AddFailure(validationFailure);
        }

        public MethodResult(IEnumerable<ValidationFailure> validationFailures)
            : this()
        {
            _failures.AddRange(validationFailures);
        }

        public IReadOnlyCollection<ValidationFailure> Failures => _failures;
        public bool Success => _failures.IsEmpty();
        public bool Failed => _failures.Any();
        public HttpStatusCode ErrorCode { get; set; }
        public object Data { get; set; }

        public void AddFailure(string errorMessage)
        {
            errorMessage = errorMessage?.Trim();

            if (errorMessage.IsEmpty())
                return;

            if (_failures.Any(x => x.PropertyName.IsEmpty() && x.ErrorMessage.EqualsIgnoreCase(errorMessage)))
                return;

            _failures.Add(new ValidationFailure(string.Empty, errorMessage));
        }

        public void AddFailure(string propertyName, string errorMessage)
        {
            propertyName = propertyName?.Trim();
            errorMessage = errorMessage?.Trim();

            if (propertyName.IsEmpty() || errorMessage.IsEmpty())
                return;

            if (_failures.Any(x => x.PropertyName.EqualsIgnoreCase(propertyName) && x.ErrorMessage.EqualsIgnoreCase(errorMessage)))
                return;

            _failures.Add(new ValidationFailure(propertyName, errorMessage));
        }

        public void AddFailure(ValidationFailure validationFailure)
        {
            if (_failures.Contains(validationFailure))
                return;

            if (_failures.Any(x =>
                x.PropertyName.EqualsIgnoreCase(validationFailure.PropertyName) &&
                x.ErrorMessage.EqualsIgnoreCase(validationFailure.ErrorMessage)))
                return;

            _failures.Add(validationFailure);
        }
    }
}