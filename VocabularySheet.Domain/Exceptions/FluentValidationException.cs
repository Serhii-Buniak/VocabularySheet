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

    public FluentValidationException(string message, IEnumerable<ValidationFailure> errors) : base(message, errors)
    {
    }

    public FluentValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public FluentValidationException(string message, IEnumerable<ValidationFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
    {
    }
}
