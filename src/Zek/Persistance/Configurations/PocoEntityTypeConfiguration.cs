using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zek.Data.Entity;
using Zek.Domain.Entities;

namespace Zek.Persistance.Configurations
{
    public class PocoEntityTypeConfiguration<TEntity> : PocoEntityTypeConfiguration<TEntity, int>
        where TEntity : PocoEntity<int>
    {

    }
    public class PocoEntityTypeConfiguration<TEntity, TId> : CreateEntityTypeConfiguration<TEntity, TId>
     where TEntity : PocoEntity<TId>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder, bool valueGeneratedOnAdd)
        {
            base.Configure(builder, valueGeneratedOnAdd);

            builder.Property(x => x.ModifiedDate).HasColumnTypeDateTime();
            builder.HasIndex(x => x.ModifierId);
        }

    }
}
