using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Base
{
    //public class TranslateModel
    //{
    //    //public TId Id { get; set; }
    //    //public TDictionary Dictionary { get; set; }

    //    public byte CultureId { get; set; }
    //    public Culture Culture { get; set; }

    //    public string Text { get; set; }
    //}


    //public class TranslateModelMap<TEntity> : EntityTypeMap<TEntity> where TEntity : TranslateModel
    //{
    //    public TranslateModelMap(ModelBuilder builder) : base(builder)
    //    {
    //        //HasKey(t => new { t.Id, t.CultureId });

    //        //HasIndex(t => t.Id);
    //        //HasOne(t => t.Dictionary).WithMany().HasForeignKey(t => t.Id).OnDelete(DeleteBehavior.Cascade);

    //        HasIndex(t => t.CultureId);
    //        HasOne(t => t.Culture).WithMany().HasForeignKey(t => t.CultureId).OnDelete(DeleteBehavior.Restrict);

    //        Property(t => t.Text).IsRequired().HasMaxLength(400);
    //        HasIndex(t => t.Text);
    //    }
    //}



    public class TranslateModel<TDictionary, TId> where TDictionary : class
    {
        public TId Id { get; set; }
        public TDictionary Dictionary { get; set; }

        public int CultureId { get; set; }

        public string Text { get; set; }
    }


    public class TranslateModelMap<TTranslateEntity, TDictionary, TId> : EntityTypeMap<TTranslateEntity>
        where TTranslateEntity : TranslateModel<TDictionary, TId>
        where TDictionary : class
    {
        public TranslateModelMap(ModelBuilder builder) : base(builder)
        {
            HasKey(t => new { t.Id, t.CultureId });

            HasOne(t => t.Dictionary).WithMany().HasForeignKey(t => t.Id).OnDelete(DeleteBehavior.Cascade);

            Property(t => t.Text).IsRequired().HasMaxLength(400);
            HasIndex(t => t.Text);
        }
    }
}
