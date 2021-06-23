using System;

namespace Ngx.Monorepo.Framework.Core.Domain
{
    /// <summary>
    /// Entity for Config table to be used in various microservices that need to store configs.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Primary key for the config.
        /// </summary>
        public Guid ConfigId { get; private set; }

        /// <summary>
        /// Name of the config.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value that the configuration has.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Type of the config, used for casting the value to proper type.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Id of the tenant the config is for.
        /// </summary>
        public Guid TenantId { get; private set; }

        /// <summary>
        /// A description of what the config is used for.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Order to display the config in, used when there are multiple config values.
        /// </summary>
        public int? Order { get; private set; }

        public Config(Guid configId, string name, string value, string type, Guid tenantId, string description, int? order)
        {
            if (configId == default) throw new ArgumentException("Value cannot be default.", nameof(configId));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));
            if (tenantId == default) throw new ArgumentException("Value cannot be default.", nameof(tenantId));
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));

            ConfigId = configId;
            Name = name;
            Value = value;
            Type = type;
            TenantId = tenantId;
            Description = description;
            Order = order;
        }

        public Config(string name, string value, string type, Guid tenantId, string description, int? order)
            : this(Guid.NewGuid(), name, value, type, tenantId, description, order) { }
    }
}
