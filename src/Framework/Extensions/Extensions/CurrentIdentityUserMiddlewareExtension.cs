using Microsoft.AspNetCore.Builder;
using Ngx.Monorepo.Framework.Core.Security;
using Ngx.Monorepo.Framework.Extensions.Middleware;

namespace Ngx.Monorepo.Framework.Extensions.Extensions
{
    public static class CurrentIdentityUserMiddlewareExtension
    {
        /// <summary>
        /// Extension method for registering <see cref="CurrentIdentityUserMiddleware"/> that populates 
        /// the <see cref="IdentityUser"/> object in DI.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/> for fluent API.</returns>
        public static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app)
        {
            app.UseMiddleware<CurrentIdentityUserMiddleware>();
            return app;
        }
    }
}
