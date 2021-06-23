using Ngx.Monorepo.Framework.Hangfire.Extensions;
using System;

namespace Ngx.Monorepo.Framework.Hangfire.Exceptions
{
    /// <summary>
    /// Exception thrown when Hangfire is trying to be used but has never been configured.
    /// </summary>
    public class HangfireNotConfiguredException : Exception
    {
        public HangfireNotConfiguredException()
            : base($"Hangfire has not been configured! Ensure you are calling {nameof(ServiceCollectionExtension.RegisterHangfire)} before attempting to use Hangfire!")
        { }
    }
}
