using FluentValidation;
using FluentValidation.Results;

namespace Tools.Validations;

public class FluentValidationException : ValidationException
{
    public FluentValidationException(string message) : base(message)
    {
    }

    public FluentValidationException(IEnumerable<ValidationFailure> errors) : base(errors)
    {
    }
}
