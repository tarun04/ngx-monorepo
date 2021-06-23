using Microsoft.Extensions.DependencyInjection;
using Ngx.Monorepo.Framework.Core.Security;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers current Identity user.
    /// </summary>
    public static class CurrentIdentityUser
    {
        public static IServiceCollection RegisterCurrentIdentityUser(this IServiceCollection services)
        {
            services.AddScoped<IdentityUser>();
            return services;
        }
    }
}
