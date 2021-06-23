using Microsoft.AspNetCore.Builder;
using Ngx.Monorepo.Framework.Extensions.Middleware;

namespace Ngx.Monorepo.Framework.Extensions.Extensions
{
    public static class ExceptionHandlingMiddlewareExtension
    {
        /// <summary>
        /// Extension method for registering <see cref="ExceptionHandlingMiddleware"/> in DI.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/> for fluent API.</returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
