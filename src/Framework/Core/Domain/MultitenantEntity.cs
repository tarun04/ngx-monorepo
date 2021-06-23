using System;

namespace Ngx.Monorepo.Framework.Core.Domain
{
    public abstract class MultitenantEntity : Entity
    {
        /// <summary>
        /// Id of the tenant.
        /// </summary>
        public Guid TenantId { get; set; }
    }
}
