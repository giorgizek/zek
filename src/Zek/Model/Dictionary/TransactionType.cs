using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class TransactionType : PocoModel<int>
    {
        //public string Code { get; set; }
    }

    public class TransactionTypeMap : PocoModelMap<TransactionType, int>
    {
        public TransactionTypeMap(ModelBuilder builder) : base(builder)
        {
            ToTable("TransactionTypes", "Dictionary");
            //Property(t => t.Code).HasMaxLength(10).IsRequired();
        }
    }



    public class TransactionTypeTranslate : TranslateModel<TransactionType, int>
    {
    }

    public class TransactionTypeTranslateMap : TranslateModelMap<TransactionTypeTranslate, TransactionType, int>
    {
        public TransactionTypeTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("TransactionTypeTranslates", "Translate");
        }
    }
}
