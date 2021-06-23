using Microsoft.Extensions.DependencyInjection;
using Ngx.Monorepo.Framework.Core.Domain;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers requesting product info.
    /// </summary>
    public static class RequestingProduct
    {
        public static IServiceCollection RegisterRequestingProduct(this IServiceCollection services)
        {
            services.AddScoped<RequestingProductInfo>();
            return services;
        }
    }
}
