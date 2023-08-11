using System.Runtime.Serialization;

namespace VocabularySheet.Domain.Exceptions.HttpClientExceptions;

public class HttpClientInternetServerException : HttpClientException
{
    public HttpClientInternetServerException() : base("500 Internal Server Error")
    {
    }

    public HttpClientInternetServerException(string? message) : base(message)
    {
    }

    public HttpClientInternetServerException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected HttpClientInternetServerException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
