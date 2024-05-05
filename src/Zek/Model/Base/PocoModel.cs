using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Domain.Entities;

namespace Zek.Model.Base
{
    public class PocoModelMap<TEntity> : PocoModelMap<TEntity, int>
        where TEntity : PocoEntity
    {
        public PocoModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
        }
    }
    public class PocoModelMap<TEntity, TId> : CreateModelMap<TEntity, TId>
        where TEntity : PocoEntity<TId>
    {
        public PocoModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            HasIndex(x => x.ModifierId);
            Property(x => x.ModifiedDate).HasColumnTypeDateTime();
        }
    }
}