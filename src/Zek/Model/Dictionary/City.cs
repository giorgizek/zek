namespace Zek.Model.Dictionary
{
    //public class City : BaseModel<int>
    //{
    //    public int CountryId { get; set; }
    //    public Country Country { get; set; }

    //    //public List<CityTranslate> Translates { get; set; }
    //}

    //public class CityMap : BaseModelMap<City, int>
    //{
    //    public CityMap(ModelBuilder builder) : base(builder)
    //    {
    //        ToTable("Cities", "Dictionary");
    //    }
    //}

    //public class CityTranslate : TranslateModel<City, int>
    //{
    //    //public int CityId { get; set; }
    //    //public City City { get; set; }
    //}

    //public class CityTranslateMap : TranslateModelMap<CityTranslate, City, int>
    //{
    //    public CityTranslateMap(ModelBuilder builder) : base(builder)
    //    {
    //        ToTable("CityTranslates", "Translate");
    //        //HasKey(t => new { t.CityId, t.CultureId });

    //        //HasIndex(t => t.CityId);
    //        //HasOne(t => t.City).WithMany().HasForeignKey(t => t.CityId).OnDelete(DeleteBehavior.Cascade);
    //    }
    //}
}