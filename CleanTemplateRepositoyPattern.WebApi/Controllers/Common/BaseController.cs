using CleanTemplateRepositoyPattern.Application.Responses;
using CleanTemplateRepositoyPattern.WebApi.ApplicationAttribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CleanTemplateRepositoyPattern.WebApi.Controllers.Common
{
     #if (!DEBUG)
     [PermissionAuthorize]
     #endif
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected OkObjectResult Ok(BaseResponse? value)
        {

            value.TraceId = HttpContext.TraceIdentifier;
            return base.Ok(value);
        }

        protected CreatedResult Created(string uri, BaseResponse? value)
        {
            return base.Created(uri, value);
        }


        protected CreatedResult Created(Uri uri, BaseResponse? value)
        {
            return base.Created(uri, value);
        }

        protected CreatedAtActionResult CreatedAtAction(string? actionName, object? routeValues, BaseResponse? value)
        {
            return base.CreatedAtAction(actionName, routeValues, value);
        }

        protected CreatedAtActionResult CreatedAtAction(string? actionName, string? controllerName, object? routeValues, BaseResponse? value)
        {
            return base.CreatedAtAction(actionName, controllerName, routeValues, value);
        }

    }
}
