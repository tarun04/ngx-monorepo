using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Ngx.Monorepo.Framework.Extensions.Attributes
{
    /// <summary>
    /// Attribute accepts an array of strings for environments. 
    /// Based on the environments passed to the attribute, this attribute will intercept a request
    /// and return Status Code 404 for the route which is decorated with it. Attribute is using 
    /// <see cref="IWebHostEnvironment"/> for the environment comparison. 
    /// </summary>
    public class SwaggerDocumentFilterAttribute : ActionFilterAttribute
    {
        public string[] Environments { get; set; }

        public SwaggerDocumentFilterAttribute(params string[] environments)
        {
            this.Environments = environments;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var hostingEnvironmentService = (IWebHostEnvironment)context.HttpContext.RequestServices.GetService(typeof(IWebHostEnvironment));

            if (this.Environments.Contains(hostingEnvironmentService.EnvironmentName))
            {
                context.Result = new StatusCodeResult(404);
            }
        }
    }
}
