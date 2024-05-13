namespace Tools.Http.Exceptions;

public class HttpClientException(string? message) : Exception(message)
{
    public HttpClientException() : this("Http client exception occur")
    {
    }
}
