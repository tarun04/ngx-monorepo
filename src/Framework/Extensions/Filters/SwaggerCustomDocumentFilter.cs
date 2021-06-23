using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Ngx.Monorepo.Framework.Extensions.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Ngx.Monorepo.Framework.Extensions.Filters
{
    /// <summary>
    /// Hide an API item decorated with the <see cref="SwaggerDocumentFilterAttribute"/> from the Swagger document.
    /// </summary>
    public class SwaggerCustomDocumentFilter : IDocumentFilter
    {
        private readonly IWebHostEnvironment env;

        public SwaggerCustomDocumentFilter(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var groupItems = context.ApiDescriptions;
            foreach (var groupItem in groupItems)
            {
                // Find the Api Description Item which was flagged with the DisableForEnv Attribute
                var disabledElements = groupItem.ActionDescriptor.FilterDescriptors.Where(x => x.Filter.GetType().Equals(typeof(SwaggerDocumentFilterAttribute)));

                // If no elements were flagged to be disabled, just continue
                if (!disabledElements.Any())
                    continue;

                // If there ARE items marked with the DisableForEnv attribute for each attribute
                foreach (var disabledItem in disabledElements)
                {
                    // Match if the current running environment matches the one flagged in the attribute
                    var filter = (SwaggerDocumentFilterAttribute)disabledItem.Filter;
                    if (filter.Environments.Contains(env.EnvironmentName))
                    {
                        // If string matches the env, remove it from the document creation
                        swaggerDoc.Paths.Remove($"/{groupItem.RelativePath}");
                    }
                }
            }
        }
    }
}
