using System.Net;

namespace Juris.Api.Exceptions;

public class HttpResponseException : Exception
{
    public HttpResponseException(HttpStatusCode statusCode, string errorMessage = "Bad Request")
    {
        (StatusCode, ErrorMessage) = (statusCode, errorMessage);
    }

    public HttpStatusCode StatusCode { get; }

    public string ErrorMessage { get; }
}