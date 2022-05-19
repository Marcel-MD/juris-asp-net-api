using System.Net;
using System.Security.Claims;
using Juris.Common.Exceptions;
using Juris.Resource;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    [NonAction]
    protected long GetCurrentUserId()
    {
        var ok = long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var currentUserId);
        if (!ok)
            throw new HttpResponseException(HttpStatusCode.BadRequest,
                GlobalResource.BadToken);

        return currentUserId;
    }
}