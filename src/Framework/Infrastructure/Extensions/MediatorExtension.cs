using MediatR;
using Ngx.Monorepo.Framework.Core.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Infrastructure.Extensions
{
    public static class MediatorExtension
    {
        /// <summary>
        /// Dispatches all domain events for Entities that are being tracked by EF Core.
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/> for publishing Domain Events as <see cref="INotification"/></param>
        /// <param name="ctx">Instance of <see cref="BaseDbContext"/></param>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, BaseDbContext ctx)
        {
            // Get all Entities in the ChangeTracker that have Domain Events.
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            if (!domainEntities.Any())
                return;

            // Merge all domain events into new List.
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            // Clear out DomainEvents.
            foreach (var domainEntity in domainEntities)
                domainEntity.Entity.ClearDomainEvents();

            // Publish Mediator Notifications.
            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }

        /// <summary>
        /// Dispatches all integration events for Entities that are being tracked by EF Core.
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/> for publishing Domain Events as <see cref="INotification"/></param>
        /// <param name="ctx">Instance of <see cref="BaseDbContext"/></param>
        public static async Task DispatchIntegrationEventsAsync(this IMediator mediator, BaseDbContext ctx)
        {
            // Get all Entities in the ChangeTracker that have Integration Events.
            var integrationEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.IntegrationEvents != null && x.Entity.IntegrationEvents.Any())
                .ToList();

            if (!integrationEntities.Any())
                return;

            // Merge all domain events into new List.
            var integrationEvents = integrationEntities
                .SelectMany(x => x.Entity.IntegrationEvents)
                .ToList();

            // Clear out IntegrationEvents.
            foreach (var integrationEntity in integrationEntities)
                integrationEntity.Entity.ClearIntegrationEvents();

            // Publish Mediator Notifications.
            foreach (var integrationEvent in integrationEvents)
                await mediator.Publish(integrationEvent);
        }
    }
}
