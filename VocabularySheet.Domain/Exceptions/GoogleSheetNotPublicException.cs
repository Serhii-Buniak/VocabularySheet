using System.Runtime.Serialization;
using VocabularySheet.Domain.Exceptions.HttpClientExceptions;

namespace VocabularySheet.Domain.Exceptions;

public class GoogleSheetNotPublicException : HttpClientException
{
    public GoogleSheetNotPublicException() : base("Google sheet is not public.")
    {
    }
}
