using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Ngx.Monorepo.Framework.Core.Domain;
using Ngx.Monorepo.Framework.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Infrastructure.Utility
{
    /// <summary>
    /// Helps in retrieving config values from database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConfigHelper<T> : IConfigHelper where T : BaseDbContext
    {
        private readonly T context;
        private readonly IMemoryCache memoryCache;
        private readonly MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));

        public ConfigHelper(T context, IMemoryCache memoryCache)
        {
            this.context = context;
            this.memoryCache = memoryCache;
        }

        /// <inheritdoc />
        public async Task<Config> TryGetConfig(string name, Guid tenantId)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (tenantId == default) throw new ArgumentException("Value cannot be default.", nameof(tenantId));

            if (memoryCache.TryGetValue($"{tenantId}_{name}", out var config) && config != null)
                return config as Config;

            config = await context.Config.FirstOrDefaultAsync(x => x.Name == name && x.TenantId == tenantId).ConfigureAwait(false);

            if (config != null)
                memoryCache.Set($"{tenantId}_{name}", config, cacheEntryOptions);

            return config as Config;
        }
    }
}
