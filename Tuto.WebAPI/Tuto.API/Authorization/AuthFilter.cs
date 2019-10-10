using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using Tuto.API.Configuration;
using Tuto.Domain.Authorization;

namespace Tuto.API.Authorization
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public AuthFilter(string role)
        {
            _role = role;
        }
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User is AppUser)
            {
                if (!context.HttpContext.User.IsInRole(_role))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
            else
            {
                RedirectToOAuthSerivce(context);
            }
        }

        private static void RedirectToOAuthSerivce(AuthorizationFilterContext context)
        {
            var config = (IOptions<OAuthConfig>)context.HttpContext.RequestServices.GetService(typeof(IOptions<OAuthConfig>));
            var fullHostName = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host.Value}";
            var requestUrl = context.HttpContext.Request.Path;
            var authorizationUrl = GoogleOAuthHelpers.GetAuthorizationUrl(requestUrl, $"{fullHostName}/OAuth", config.Value.ClientId);
            context.Result = new RedirectResult(authorizationUrl);
        }
    }
}
