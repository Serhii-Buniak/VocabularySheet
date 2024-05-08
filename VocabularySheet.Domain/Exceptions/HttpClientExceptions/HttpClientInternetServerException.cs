using System.Runtime.Serialization;

namespace VocabularySheet.Domain.Exceptions.HttpClientExceptions;

public class HttpClientInternetServerException : HttpClientException
{
    public HttpClientInternetServerException() : base("500 Internal Server Error")
    {
    }
}
