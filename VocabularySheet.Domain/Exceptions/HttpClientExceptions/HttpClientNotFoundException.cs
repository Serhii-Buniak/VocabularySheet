using System.Runtime.Serialization;

namespace VocabularySheet.Domain.Exceptions.HttpClientExceptions;

public class HttpClientNotFoundException : HttpClientException
{
    public HttpClientNotFoundException() : base("404 Not Found")
    {
    }
}
