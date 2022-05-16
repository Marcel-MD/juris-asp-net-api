using Juris.Api.Middlewares;

namespace Juris.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseDbTransaction(this IApplicationBuilder app)
    {
        return app.UseMiddleware<DbTransactionMiddleware>();
    }
}