using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Base
{
    public class PocoModel : PocoModel<int> { }
    public class PocoModel<TId> : CreateModel<TId>
    {
        public int? ModifierId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }



    public class PocoModelMap<TEntity> : PocoModelMap<TEntity, int>
        where TEntity : PocoModel
    {
        public PocoModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
        }
    }
    public class PocoModelMap<TEntity, TId> : CreateModelMap<TEntity, TId>
        where TEntity : PocoModel<TId>
    {
        public PocoModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            HasIndex(x => x.ModifierId);
            Property(x => x.ModifiedDate).HasColumnTypeDateTime();
        }
    }
}