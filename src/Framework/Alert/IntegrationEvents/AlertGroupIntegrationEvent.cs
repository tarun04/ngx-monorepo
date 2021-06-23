using Ngx.Monorepo.Framework.Alert.Enums;
using Ngx.Monorepo.Framework.Core.Enums;
using System;

namespace Ngx.Monorepo.Framework.Alert.IntegrationEvents
{
    /// <summary>
    /// Integration Event used to push Group Alerts from various systems
    /// </summary>
    public class AlertGroupIntegrationEvent
    {
        /// <summary>
        /// Alert Title, e.g. Application Submitted
        /// </summary>
        public string AlertTitle { get; set; }

        /// <summary>
        /// Details of the alert
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        /// Alert Severity - info, warn, success, error
        /// </summary>
        public AlertSeverity AlertSeverity { get; set; }

        /// <summary>
        /// Product name for which the alert occured
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Particular entity ID related with the Alert, e.g. application file
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Value for the given key
        /// </summary>
        public Guid Value { get; set; }

        /// <summary>
        /// Tenant ID for which the alert occured
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// List of Groups that should receive the alert
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Entity Type related to the Event
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// User id sending the alert
        /// </summary>
        public Guid UserId { get; set; }
    }
}
