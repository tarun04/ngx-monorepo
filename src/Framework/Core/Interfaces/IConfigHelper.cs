using Ngx.Monorepo.Framework.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Core.Interfaces
{
    public interface IConfigHelper
    {
        /// <summary>
        /// Attempts to get a config from the cache.
        /// If not found in cache it will retrieve from the database and add it to the cache.
        /// </summary>
        /// <param name="name">Name of the <see cref="Config"/></param>
        /// <param name="tenantId">Id of the tenant to get the <see cref="Config"/> for.</param>
        /// <returns><see cref="Config"/></returns>
        Task<Config> TryGetConfig(string name, Guid tenantId);
    }
}
