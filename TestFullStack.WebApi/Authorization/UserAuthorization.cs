using JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Utils;

namespace TestFullStack.WebApi.Authorization
{
    public class UserAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            Token token = null;
            try
            {
                token = actionContext.HttpContext.GetToken();
            }
            catch (SignatureVerificationException)
            {
                actionContext.Result = new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                actionContext.Result = new StatusCodeResult(500);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
