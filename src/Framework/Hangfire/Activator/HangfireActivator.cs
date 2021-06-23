using Hangfire;
using System;

namespace Ngx.Monorepo.Framework.Hangfire.Activator
{
    /// <summary>
    /// Activator to use Dependency Injection with Hangfire.
    /// https://docs.hangfire.io/en/latest/background-methods/using-ioc-containers.html
    /// </summary>
    public class HangfireActivator : JobActivator
    {
        private readonly IServiceProvider serviceProvider;

        public HangfireActivator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return serviceProvider.GetService(type);
        }
    }
}
