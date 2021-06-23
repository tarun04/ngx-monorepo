using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Ngx.Monorepo.Framework.Core.Extensions;
using Ngx.Monorepo.Framework.Core.Security;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Extensions.Middleware
{
    /// <summary>
    /// Middleware that limits a User's access to Gateways based on the Products they have available in their Claims.
    /// </summary>
    public class ProductAuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ProductConfig productConfig;

        public ProductAuthorizationMiddleware(RequestDelegate next, IConfiguration config)
        {
            this.next = next;
            productConfig = config.GetSection("Product").Get<ProductConfig>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // If the user is not authenticated let them through, they will either be bounced by security or the resource they are requesting is publicly available.
            if (httpContext.User.Identity.IsAuthenticated)
            {
                // Get Products from their claims.
                var productsJson = httpContext.User.Claims.FirstOrDefault(x => x.Type == "Products")?.Value;
                // If they have no products claim return 403 forbidden.
                if (string.IsNullOrEmpty(productsJson))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
                // Check if they have this product in their claims, if not return forbidden.
                var productsDictionary = productsJson.FromJson<UserProduct[]>();
                if (!productsDictionary.Any(x => x.Name.Equals(productConfig.ProductName, StringComparison.OrdinalIgnoreCase)))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }
            httpContext.Request.Headers.Add("Requesting-Product", productConfig.ProductName);
            httpContext.Request.Headers.Add("Requesting-ProductId", productConfig.ProductId);
            await next.Invoke(httpContext);
        }
    }

    public class ProductConfig
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
