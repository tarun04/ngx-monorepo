using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Ngx.Monorepo.Framework.Extensions.Helpers;
using System;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Health Check Middleware and libraries for reporting the health of app infrastructure components.
    /// </summary>
    public static class HealthChecks
    {
        /// <summary>
        ///  Periodically executes app's availability and confirms that the app can communicate with the database using DbContext.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterHealthChecks<T>(this IServiceCollection services, IWebHostEnvironment env) where T : DbContext
        {
            if (env.IsProduction())
            {
                // we mark with 'ready' tag all long lasting health checks so we can call them with the separate middleware route
                services.AddHealthChecks()
                        .AddDbContextCheck<T>(tags: new[] { "ready" });

                // If needed, we can change default values of periodically health check calls
                services.Configure<HealthCheckPublisherOptions>(options =>
                {
                    options.Delay = TimeSpan.FromSeconds(5);

                    // there is an issue in Core 2.2 with setting 'Period' which will be fixed in Core 3.0
                    // options.Period = TimeSpan.FromSeconds(30);

                    options.Predicate = (check) => check.Tags.Contains("ready");
                });

                // The following workaround permits adding an IHealthCheckPublisher 
                // instance to the service container when one or more other hosted 
                // services have already been added to the app. This workaround
                // won't be required with the release of ASP.NET Core 3.0. For more 
                // information, see: https://github.com/aspnet/Extensions/issues/639.
                services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IHostedService),
                                          typeof(HealthCheckPublisherOptions).Assembly.GetType("Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckPublisherHostedService")));

                // we need this line so the health checks could be called periodically by it selfs
                services.AddSingleton<IHealthCheckPublisher, ReadinessPublisher>();
            }
            return services;
        }
    }
}
