using System.Net;

namespace Juris.Common.Exceptions;

/// <summary>
///     Describes an exception that occurred during the processing of HTTP requests.
/// </summary>
public class HttpResponseException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the HttpResponseException class. By default has status code 404 and error message
    ///     "Bad Request"
    /// </summary>
    /// <param name="statusCode">The http status code.</param>
    /// <param name="errorMessage">The error message.</param>
    public HttpResponseException(HttpStatusCode statusCode, string errorMessage = "Bad Request")
    {
        (StatusCode, ErrorMessage) = (statusCode, errorMessage);
    }

    public HttpStatusCode StatusCode { get; }

    public string ErrorMessage { get; }
}