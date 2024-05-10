using FluentValidation;
using FluentValidation.Results;
using System.Runtime.Serialization;

namespace VocabularySheet.Domain.Exceptions;

public class FluentValidationException : ValidationException
{
    public FluentValidationException(string message) : base(message)
    {
    }

    public FluentValidationException(IEnumerable<ValidationFailure> errors) : base(errors)
    {
    }

}
