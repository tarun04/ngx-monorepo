using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngx.Monorepo.Framework.Core.Utility;
using System;
using System.Net.Http.Headers;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers http clients, set default client settings.
    /// </summary>
    public static class HttpClients
    {
        public static IServiceCollection RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var microservicesWrapper = configuration.Get<MicroservicesHelper>();
            foreach (var microservice in microservicesWrapper.Microservices)
            {
                var baseUrl = $"{microservice.BaseUrl.TrimEnd('/')}";

                services.AddHttpClient(microservice.Name, c =>
                {
                    c.BaseAddress = new Uri(baseUrl);
                    c.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));
            }
            return services;
        }
    }
}
