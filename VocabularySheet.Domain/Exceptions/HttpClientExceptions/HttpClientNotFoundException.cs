using System.Runtime.Serialization;

namespace VocabularySheet.Domain.Exceptions.HttpClientExceptions;

public class HttpClientNotFoundException : HttpClientException
{
    public HttpClientNotFoundException() : base("")
    {
    }

    public HttpClientNotFoundException(string? message) : base(message)
    {
    }

    public HttpClientNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected HttpClientNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
