using MediatR;
using System;
using System.Collections.Generic;

namespace Ngx.Monorepo.Framework.Core.Domain
{
    public abstract class Entity
    {
        /// <summary>
        /// Name of the user who created the entity.
        /// </summary>
        public string CreatedByName { get; protected set; }

        /// <summary>
        /// Id of the user who created the entity.
        /// </summary>
        public Guid CreatedByUserId { get; protected set; }

        /// <summary>
        /// Created date of the entity.
        /// </summary>
        public DateTime CreatedDate { get; protected set; }

        /// <summary>
        /// Name of the user who modified the entity.
        /// </summary>
        public string ModifiedByName { get; protected set; }

        /// <summary>
        /// Id of the user who modified the entity.
        /// </summary>
        public Guid ModifiedByUserId { get; protected set; }

        /// <summary>
        /// Modified date of the entity.
        /// </summary>
        public DateTime ModifiedDate { get; protected set; }

        /// <summary>
        /// Sets created and modified values of an entity.
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        public void SetCreatedAndModified(DateTime currentTime, Guid userId, string username)
        {
            CreatedByName = username;
            CreatedByUserId = userId;
            CreatedDate = currentTime;
            ModifiedByName = username;
            ModifiedByUserId = userId;
            ModifiedDate = currentTime;
        }

        /// <summary>
        /// Sets modified values of an entity.
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        public void SetModified(DateTime currentTime, Guid userId, string username)
        {
            ModifiedByName = username;
            ModifiedByUserId = userId;
            ModifiedDate = currentTime;
        }

        #region Domain Events

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        #endregion

        #region Integration Events

        private List<object> _integrationEvents;
        public IReadOnlyCollection<object> IntegrationEvents => _integrationEvents?.AsReadOnly();

        public void AddIntegrationEvent(object eventItem)
        {
            _integrationEvents ??= new List<object>();
            _integrationEvents.Add(eventItem);
        }

        public void RemoveIntegrationEvent(object eventItem)
        {
            _integrationEvents?.Remove(eventItem);
        }

        public void ClearIntegrationEvents()
        {
            _integrationEvents?.Clear();
        }
        #endregion
    }
}
