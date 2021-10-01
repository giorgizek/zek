using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class Currency : BaseModel<int>
    {
        public string Code { get; set; }
        public string Symbol { get; set; }

        //public List<CurrencyTranslate> Translates { get; set; }
    }

    public class CurrencyMap : BaseModelMap<Currency, int>
    {
        public CurrencyMap(ModelBuilder builder) : base(builder, false)
        {
            ToTable("Currencies", "Dictionary");
            Property(t => t.Code).HasMaxLength(10).IsRequired();
            HasIndex(t => t.Code).IsUnique();
            Property(t => t.Symbol).HasMaxLength(10).IsRequired();
        }
    }




    //public class CurrencyTranslate : TranslateModel<Currency, int>
    //{
    //    public string MinorUnit { get; set; }
    //}

    //public class CurrencyTranslateMap : TranslateModelMap<CurrencyTranslate, Currency, int>
    //{
    //    public CurrencyTranslateMap(ModelBuilder builder) : base(builder)
    //    {
    //        ToTable("CurrencyTranslates", "Translate");
    //    }
    //}
}
