using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ngx.Monorepo.Framework.Extensions.Filters;
using System;
using System.Collections.Generic;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers Swagger support based on the version numbers.
    /// </summary>
    public static class Swagger
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services, string title, Action<IList<string>> includeCommentsPaths = null)
        {
            services.AddSwaggerGen(
                c =>
                {
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerDoc(
                            description.GroupName,
                            new OpenApiInfo()
                            {
                                Title = $"{title}",
                                Version = description.ApiVersion.ToString()
                            });
                    }
                    includeCommentsPaths = IncludeCommentsInSwagger =>
                    {
                        if (includeCommentsPaths != null)
                        {
                            foreach (var item in IncludeCommentsInSwagger)
                            {
                                c.IncludeXmlComments(item);
                            }
                        }
                    };
                    c.DocumentFilter<SwaggerCustomDocumentFilter>();
                });
            return services;
        }
    }
}
