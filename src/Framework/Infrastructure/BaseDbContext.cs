using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ngx.Monorepo.Framework.Core.Domain;
using Ngx.Monorepo.Framework.Core.Interfaces;
using Ngx.Monorepo.Framework.Core.Security;
using Ngx.Monorepo.Framework.Core.Utility;
using Ngx.Monorepo.Framework.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Infrastructure
{
    /// <summary>
    /// Base DbContext for Microservice DbContexts. Handles dispatching Domain events.
    /// </summary>
    public class BaseDbContext : DbContext
    {
        private readonly IMediator mediator;
        private readonly IdentityUser user;
        private readonly IDateTime dateTime;
        private const string UnknownUser = "Unknown";
        public DbSet<Config> Config { get; set; }

        public BaseDbContext(DbContextOptions options)
            : this(options, new NoMediator(), new IdentityUser(), new DefaultNgxMonorepoDateTime())
        {
        }

        public BaseDbContext(DbContextOptions options, IMediator mediator, IdentityUser user, IDateTime dateTime)
            : base(options)
        {
            this.mediator = mediator;
            this.user = user;
            this.dateTime = dateTime;
        }

        public BaseDbContext(DbContextOptions options, IMediator mediator, IDateTime dateTime)
            : base(options)
        {
            this.mediator = mediator;
            this.dateTime = dateTime;
            user = new IdentityUser();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await mediator.DispatchDomainEventsAsync(this);
            await mediator.DispatchIntegrationEventsAsync(this);
            SetCreatedAndModified();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            await mediator.DispatchDomainEventsAsync(this);
            await mediator.DispatchIntegrationEventsAsync(this);
            SetCreatedAndModified();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }

        public override int SaveChanges()
        {
            mediator.DispatchDomainEventsAsync(this).Wait();
            mediator.DispatchIntegrationEventsAsync(this).Wait();
            SetCreatedAndModified();
            var result = base.SaveChanges();
            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            mediator.DispatchDomainEventsAsync(this).Wait();
            mediator.DispatchIntegrationEventsAsync(this).Wait();
            SetCreatedAndModified();
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            return result;
        }

        public void DetachAllEntities()
        {
            this.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            this.ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;

            EntityEntry[] entityEntries = this.ChangeTracker.Entries().ToArray();
            foreach (EntityEntry entityEntry in entityEntries)
            {
                entityEntry.State = EntityState.Detached;
            }
        }

        private void SetCreatedAndModified()
        {
            var currentTime = dateTime.Now;

            foreach (var entity in ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added && x.Entity is Entity).Select(x => x.Entity as Entity))
            {
                if (user != null && !user.IsLoaded)
                    entity?.SetCreatedAndModified(currentTime, Guid.Empty, UnknownUser);
                else
                    entity?.SetCreatedAndModified(currentTime, user.UserId, user.GivenName);
            }

            foreach (var entity in ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified && x.Entity is Entity).Select(x => x.Entity as Entity))
            {
                if (user != null && !user.IsLoaded)
                    entity?.SetModified(currentTime, Guid.Empty, UnknownUser);
                else
                    entity?.SetModified(currentTime, user.UserId, user.GivenName);

            }
        }
    }

    public class NoMediator : IMediator
    {
        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(TResponse));
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object>(default);
        }
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<object> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
