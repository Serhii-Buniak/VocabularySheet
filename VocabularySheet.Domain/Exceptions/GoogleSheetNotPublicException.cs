using System.Runtime.Serialization;
using VocabularySheet.Domain.Exceptions.HttpClientExceptions;

namespace VocabularySheet.Domain.Exceptions;

public class GoogleSheetNotPublicException : HttpClientException
{
    public GoogleSheetNotPublicException() : base("Google sheet is not public.")
    {
    }

    public GoogleSheetNotPublicException(string? message) : base(message)
    {
    }

    public GoogleSheetNotPublicException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected GoogleSheetNotPublicException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
