using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers versioning strategy for the Api.
    /// </summary>
    public static class ApiVersioning
    {
        public static IServiceCollection RegisterApiVersioning(this IServiceCollection services, Action<IList<string>> includeCommentsPaths = null)
        {
            services.AddApiVersioning(
               o =>
               {
                   o.AssumeDefaultVersionWhenUnspecified = true;
                   o.DefaultApiVersion = new ApiVersion(1, 0);

                   // reports if the api version is deprecated
                   o.ReportApiVersions = true;
               });
            // Group the Api versions
            var apiExplorer = services.AddVersionedApiExplorer(
               options =>
               {
                   options.GroupNameFormat = "'v'VVV";
                   options.SubstituteApiVersionInUrl = true;
               });
            return services;
        }
    }
}
