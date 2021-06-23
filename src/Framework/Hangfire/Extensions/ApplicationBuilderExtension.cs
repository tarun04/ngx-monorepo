using Hangfire;
using Microsoft.AspNetCore.Builder;
using Ngx.Monorepo.Framework.Hangfire.Activator;
using Ngx.Monorepo.Framework.Hangfire.Configuration;
using System;

namespace Ngx.Monorepo.Framework.Hangfire.Extensions
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Sets up Hangfire to be used within a Microservice.
        /// </summary>
        /// <param name="applicationBuilder">Application Builder to use.</param>
        /// <param name="config">Method to execute to register recurring jobs.</param>
        /// <returns>IApplicationBuilder for fluent api calls.</returns>
        public static IApplicationBuilder UseHangfire(this IApplicationBuilder applicationBuilder, Action<HangfireConfiguration> config)
        {
            // Set up hangfire DI.
            GlobalConfiguration.Configuration
                .UseActivator(new HangfireActivator(applicationBuilder.ApplicationServices));
            // Start using Hangfire Dashboard and Server.
            applicationBuilder.UseHangfireDashboard();
            applicationBuilder.UseHangfireServer();
            // Execute Config method.
            config(new HangfireConfiguration());
            return applicationBuilder;
        }
    }
}
