using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ngx.Monorepo.Framework.Core.Domain;

namespace Ngx.Monorepo.Framework.Infrastructure.EntityConfigurations
{
    public abstract class MultiTenantEntityConfiguration<T> : EntityConfiguration<T> where T : MultitenantEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.TenantId).IsRequired();
        }
    }
}
