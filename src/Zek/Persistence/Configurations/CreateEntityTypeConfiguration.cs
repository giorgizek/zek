using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zek.Domain.Entities;

namespace Zek.Persistence.Configurations
{
    public class CreateEntityTypeConfiguration<T> : CreateEntityTypeConfiguration<T, int>
        where T : CreateEntity
    {
    }

    public class CreateEntityTypeConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
        where TEntity : CreateEntity<TId>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            Configure(builder, true);
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder, bool valueGeneratedOnAdd)
        {
            builder.HasKey(x => x.Id);
            if (valueGeneratedOnAdd)
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            else
                builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.CreateDate).HasColumnTypeDateTime();

            builder.HasIndex(x => x.IsDeleted);
            builder.HasIndex(x => x.CreatorId);
        }
    }
}
