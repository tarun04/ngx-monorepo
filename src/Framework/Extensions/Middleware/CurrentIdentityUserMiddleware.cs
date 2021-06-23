using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ngx.Monorepo.Framework.Core.Security;
using System;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Extensions.Middleware
{
    /// <summary>
    /// Middleware for populating the Scoped IdentityUser in a request.
    /// Takes claim data forwarded by Ocelot and sets the current user.
    /// </summary>
    public class CurrentIdentityUserMiddleware
    {
        private readonly RequestDelegate next;
        private const string AllowAnonymous = "AllowAnonymous";

        public CurrentIdentityUserMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if ((!httpContext.Request.Headers.TryGetValue(AllowAnonymous, out var allowAnonymous) ||
                allowAnonymous.ToString().Equals("false", StringComparison.OrdinalIgnoreCase)) &&
                (!httpContext.Request.Path.Value.Contains("hangfire") &&
                !httpContext.Request.Path.Value.Contains("favicon") &&
                !httpContext.Request.Path.Value.Contains("signalr")))
            {
                // Get Identity User from DI.
                var currentUser = httpContext.RequestServices.GetRequiredService<IdentityUser>();
                // Set values of current identity user.
                currentUser.SetUser(httpContext.Request.Headers);
            }
            await next.Invoke(httpContext);
        }
    }
}
