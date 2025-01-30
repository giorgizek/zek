using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Domain.Entities;
using Zek.Persistence.Configurations;

namespace Zek.Model.Base
{
    [Obsolete]
    public class CreateModelMap<TEntity> : CreateModelMap<TEntity, int>
        where TEntity : CreateEntity
    {
        public CreateModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
        }
    }

    [Obsolete]
    public class CreateModelMap<TEntity, TId> : EntityTypeMap<TEntity>
        where TEntity : CreateEntity<TId>
    {
        public CreateModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder)
        {
            HasKey(x => x.Id);
            if (valueGeneratedOnAdd.GetValueOrDefault(true))
                Property(x => x.Id).ValueGeneratedOnAdd();
            else
                Property(x => x.Id).ValueGeneratedNever();

            Property(x => x.CreateDate).HasColumnTypeDateTime();

            HasIndex(x => x.IsDeleted);
            HasIndex(x => x.CreatorId);
        }
    }
}