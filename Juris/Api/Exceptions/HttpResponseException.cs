using System.Net;

namespace Juris.Api.Exceptions;

public class HttpResponseException : Exception
{
    public HttpResponseException(HttpStatusCode statusCode, string responseMessage = "Bad Request") =>
        (StatusCode, ResponseMessage) = (statusCode, responseMessage);

    public HttpStatusCode StatusCode { get; }
    
    public string ResponseMessage { get; }
}