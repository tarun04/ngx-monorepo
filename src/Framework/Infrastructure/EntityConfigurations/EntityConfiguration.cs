using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ngx.Monorepo.Framework.Core.Domain;

namespace Ngx.Monorepo.Framework.Infrastructure.EntityConfigurations
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.CreatedByName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.CreatedByUserId).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.ModifiedByName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ModifiedByUserId).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();
        }
    }
}
