using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Ngx.Monorepo.Framework.Core.Security;

namespace Ngx.Monorepo.Framework.Extensions.Attributes
{
    public sealed class IdentityUserFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Get Identity User from DI.
            var currentUser = context.HttpContext.RequestServices.GetRequiredService<IdentityUser>();

            if (!currentUser.IsLoaded)
            {
                // Set values of current identity user.
                currentUser.SetUser(context.HttpContext.Request.Headers);
            }
        }
    }
}
