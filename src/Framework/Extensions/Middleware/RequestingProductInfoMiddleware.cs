using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ngx.Monorepo.Framework.Core.Domain;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Extensions.Middleware
{
    /// <summary>
    /// Middleware for populating the RequestingProductInfo in a request.
    /// Takes claim data forwarded by Ocelot and sets the product info.
    /// </summary>
    public class RequestingProductInfoMiddleware
    {
        private readonly RequestDelegate next;

        public RequestingProductInfoMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Path.Value.Contains("hangfire") && !httpContext.Request.Path.Value.Contains("favicon"))
            {
                // Get Requesting ProductInfo from DI.
                var requestingProductInfo = httpContext.RequestServices.GetRequiredService<RequestingProductInfo>();
                // Set values of product info.
                requestingProductInfo.SetProductInfoData(httpContext.Request.Headers);
            }
            await next.Invoke(httpContext);
        }
    }
}
