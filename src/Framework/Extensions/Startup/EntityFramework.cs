using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers DbContext for a given connection.
    /// </summary>
    public static class EntityFramework
    {
        public static IServiceCollection RegisterDbContext<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            services.AddDbContext<T>(options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                        options.EnableDetailedErrors(true);
                    });
            return services;
        }
    }
}
