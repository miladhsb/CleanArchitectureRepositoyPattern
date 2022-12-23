using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace CleanTemplateRepositoyPattern.WebApi.ApplicationAttribute
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public  void OnAuthorization(AuthorizationFilterContext context)
        {
            var services = context.HttpContext.RequestServices.CreateScope().ServiceProvider;
            var Environment = services.GetRequiredService<IWebHostEnvironment>();
            var user = context.HttpContext.User;
            var FullPath = context.HttpContext.Request.Path;
   

                if (!user.Claims.Where(p => p.Type == "permission").Select(p => p.Value).Any(s => s.Equals(FullPath)))
                {

                    context.Result = new ChallengeResult(AuthenticationSchemes);
                }


              
            
        }
    }
}
