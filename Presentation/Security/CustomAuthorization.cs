using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Presentation.Models.Common.Security;
namespace Presentation.Security
{
    public class CustomAuthorization: IAuthorizationRequirement
    {

    }
    public class CustomHandler :
    AuthorizationHandler<CustomAuthorization>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CustomAuthorization requirement)
        {
            //var authFilterContext = context.Resource as AuthorizationFilterContext;
            //if (authFilterContext == null)
            //{
            //    return Task.CompletedTask;
            //}
            //SessionModel sessionValue = null;
            //var session = authFilterContext.HttpContext.Session as ISession;
            //if (session != null)
            //{
            //    sessionValue = session.GetObjectFromJson<SessionModel>(SessionContext._Session);
            //}
            ////string loggedInAdminId =
            ////    context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            ////string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

            //if (sessionValue != null && !string.IsNullOrEmpty(sessionValue.Email))
            //{
                context.Succeed(requirement);
            //}

            return Task.CompletedTask;
        }
    }
}
