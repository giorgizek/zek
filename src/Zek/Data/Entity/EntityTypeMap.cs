using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zek.Data.Entity
{
    [Obsolete("Use IEntityTypeConfiguration", false)]
    public class EntityTypeMap<TEntity>
        where TEntity : class
    {
        public EntityTypeMap(ModelBuilder builder)
        {
            EntityTypeBuilder = builder.Entity<TEntity>();
        }

        public EntityTypeBuilder<TEntity> EntityTypeBuilder { get; }
        public EntityTypeBuilder<TEntity> ToTable(string name) => EntityTypeBuilder.ToTable(name);
        public EntityTypeBuilder<TEntity> ToTable(string name, string schema) => EntityTypeBuilder.ToTable(name, schema);

        public PropertyBuilder<TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression) => EntityTypeBuilder.Property(propertyExpression);

        public KeyBuilder HasKey(Expression<Func<TEntity, object>> keyExpression) => EntityTypeBuilder.HasKey(keyExpression);
        public IndexBuilder HasIndex(Expression<Func<TEntity, object>> indexExpression) => EntityTypeBuilder.HasIndex(indexExpression);

        public EntityTypeBuilder<TEntity> Ignore(Expression<Func<TEntity, object>> propertyExpression) => EntityTypeBuilder.Ignore(propertyExpression);

        public CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> collection = null) where TRelatedEntity : class => EntityTypeBuilder.HasMany(collection);
        public ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> reference = null) where TRelatedEntity : class => EntityTypeBuilder.HasOne(reference);
    }
}
