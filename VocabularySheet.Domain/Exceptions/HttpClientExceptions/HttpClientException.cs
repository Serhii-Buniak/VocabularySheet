using System.Runtime.Serialization;

namespace VocabularySheet.Domain.Exceptions.HttpClientExceptions;

public class HttpClientException : Exception
{
    public HttpClientException() : base("Http client exception occur")
    {
    }

    public HttpClientException(string? message) : base(message)
    {
    }
}
