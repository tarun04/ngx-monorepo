using Ngx.Monorepo.Framework.Alert.Enums;
using Ngx.Monorepo.Framework.Core.Enums;
using System;
using System.Collections.Generic;

namespace Ngx.Monorepo.Framework.Alert.IntegrationEvents
{
    /// <summary>
    /// Integration Event used to push Alerts from various systems
    /// </summary>
    public class AlertIntegrationEvent
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
        /// Product Name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Particular Key for an entity: e.g., jobRequsitionId, applicationId
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Value for the particular key
        /// </summary>
        public Guid Value { get; set; }

        /// <summary>
        /// Tenant ID for which the alert occured
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// List of the users that should receive the alert
        /// </summary>
        public IReadOnlyList<Guid?> Users { get; set; }

        /// <summary>
        /// List of Claims that should receive the alert
        /// </summary>
        public IReadOnlyList<string> Claims { get; set; }

        /// <summary>
        /// Custom properties that can be sent
        /// </summary>
        public IDictionary<string, object> PropertyBag { get; set; }

        /// <summary>
        /// Entity Type related to the Event
        /// </summary>
        public EntityType EntityType { get; set; }
    }
}
