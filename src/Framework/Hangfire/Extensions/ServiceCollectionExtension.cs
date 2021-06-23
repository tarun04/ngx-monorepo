using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ngx.Monorepo.Framework.Hangfire.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Sets up Hangfire in Configure Services.
        /// </summary>
        /// <param name="serviceCollection">Service collection to register Hangfire With.</param>
        /// <param name="configuration">Configuration used to get DefaultConnection for the Database.</param>
        /// <returns>IServiceCollection for fluent api calls.</returns>
        public static IServiceCollection RegisterHangfire(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"),
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.FromSeconds(30),
                            UseRecommendedIsolationLevel = true,
                            UsePageLocksOnDequeue = true,
                            DisableGlobalLocks = true
                        });
            });
            serviceCollection.AddHangfireServer();
            return serviceCollection;
        }
    }
}
