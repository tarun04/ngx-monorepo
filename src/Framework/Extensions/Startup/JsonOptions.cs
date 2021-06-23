using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ngx.Monorepo.Framework.Extensions.Startup
{
    public static class JsonOptions
    {
        /// <summary>
        /// Registers JSON options for the system.
        /// 1. Ignores reference loop handling to prevent unexpected errors.
        /// 2. Converts DateTimes to ISO Standard UTC Datetime for easy local conversion client side.
        /// 3. Sets contract resolver to camel case to match JS naming convention.
        /// </summary>
        /// <param name="builder"><see cref="IMvcBuilder"/> to apply JSON options too.</param>
        /// <returns><see cref="IMvcBuilder"/> for fluent API usage.</returns>
        public static IMvcBuilder RegisterJsonOptions(this IMvcBuilder builder)
        {
            builder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            return builder;
        }
    }
}
