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

    public HttpClientException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected HttpClientException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
