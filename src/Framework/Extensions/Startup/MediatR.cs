using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers handlers and mediator types from the specified assemblies.
    /// </summary>
    public static class MediatR
    {
        public static IServiceCollection RegisterMediatR(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            return services;
        }
    }
}
