using Microsoft.EntityFrameworkCore;
using System;
using Zek.Data.Entity;

namespace Zek.Model.Base
{
    public class CreateModel : CreateModel<int> { }
    public class CreateModel<TId>
    {
        public TId Id { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }
    }


    public class CreateModelMap<TEntity> : CreateModelMap<TEntity, int>
        where TEntity : CreateModel
    {
        public CreateModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
        }
    }
    public class CreateModelMap<TEntity, TId> : EntityTypeMap<TEntity>
        where TEntity : CreateModel<TId>
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