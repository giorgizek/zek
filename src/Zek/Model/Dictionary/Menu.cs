namespace Zek.Model.Dictionary
{
//    public class Menu : BaseModel<int>
//    {
//        public int? ParentId { get; set; }

//        public Menu Parent { get; set; }
//        public List<Menu> Children { get; set; }

//        public string ControllerName { get; set; }

//        public string ActionName { get; set; }

//        public string Url { get; set; }

//        public List<MenuTranslate> Translates { get; set; }
//    }


//    public class MenuMap : BaseModelMap<Menu, int>
//    {
//        public MenuMap(ModelBuilder builder) : base(builder)
//        {
//            ToDictionaryTable();
//            HasIndex(t => t.ParentId);
//            HasOne(t => t.Parent).WithMany(t => t.Children).HasForeignKey(t => t.ParentId).OnDelete(DeleteBehavior.Restrict);

//            Property(t => t.ControllerName).HasMaxLength(200);
//            Property(t => t.ActionName).HasMaxLength(200);
//            HasIndex(t => new { t.ControllerName, t.ActionName });

//            Property(t => t.Url).HasMaxLength(1024);
//        }
//    }





    //public class MenuTranslate : TranslateModel
    //{
    //    public int MenuId { get; set; }
    //    public Menu Menu { get; set; }
    //}

    //public class MenuTranslateMap : TranslateModelMap<MenuTranslate>
    //{
    //    public MenuTranslateMap(ModelBuilder builder) : base(builder)
    //    {
    //        ToTranslateTable();
    //        HasKey(t => new { t.MenuId, t.CultureId });

    //        Property(t => t.MenuId).IsRequired();
    //        HasIndex(t => t.MenuId);
    //        HasOne(t => t.Menu).WithMany(t => t.Translates).HasForeignKey(t => t.MenuId).OnDelete(DeleteBehavior.Cascade);
    //    }
    //}
}
