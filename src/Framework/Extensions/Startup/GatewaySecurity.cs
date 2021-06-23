using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    public static class GatewaySecurity
    {
        /// <summary>
        /// Sets up the proper JWT Bearer token settings for integration into both B2B and B2C identity servers.
        /// This requires settings be provided in configuration like the below:
        /// "IdentityServer": {
        ///     StsServer": "https://localhost:5001",
        ///     "ApiName": "admin",
        ///     "Secret": "E9CAC93D-88CA-4DE7-8C86-D8BC2F35D06B"
        ///  }
        /// This also requires app.UseAuthentication to be called in Configure App.
        /// </summary>
        /// <param name="services">Services collection to register authentication and IS4 with.</param>
        /// <param name="configuration">IConfiguration that stores identity server settings.</param>
        /// <returns>Service collection back for fluent api.</returns>
        /// <exception cref="Exception">Thorwn if any of the identity server settings are null or white space.</exception>
        public static IServiceCollection RegisterGatewaySecurity(this IServiceCollection services, IConfiguration configuration)
        {
            // Get settings needed from Configuration
            var authority = configuration["IdentityServer:StsServer"];
            var apiName = configuration["IdentityServer:ApiName"];
            var secret = configuration["IdentityServer:Secret"];

            // Validate settings.
            if (string.IsNullOrWhiteSpace(authority))
                throw new Exception("Authority cannot be null or empty! Set IdentityServer: StsServer in app settings.");

            if (string.IsNullOrWhiteSpace(apiName))
                throw new Exception("ApiName cannot be null or empty! Set IdentityServer: ApiName in app settings.");

            if (string.IsNullOrWhiteSpace(secret))
                throw new Exception("Secret cannot be null or empty! Set IdentityServer: Secret in app settings.");

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                });
            return services;
        }
    }
}
