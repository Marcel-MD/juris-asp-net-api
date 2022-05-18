using Juris.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Juris.Api.Filters;

/// <summary>
///     Handles an <b>HttpResponseException</b> and creates relevant http response with the status code and error message
///     specified in the exception.
/// </summary>
public class HttpResponseExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled) return;

        if (context.Exception is HttpResponseException httpResponseException)
        {
            var responseBody = new
            {
                errors = new[] {httpResponseException.ErrorMessage}
            };

            context.Result = new ObjectResult(responseBody)
            {
                StatusCode = (int) httpResponseException.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }
}