using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Juris.Api.Exceptions;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
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

    public int Order => int.MaxValue - 10;
}