using Zek.Model.Identity;

namespace Zek.Model.Dictionary
{
    public class PanelBar
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public PanelBar Parent { get; set; }
        public List<PanelBar> Children { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Url { get; set; }

        public List<PanelBarTranslate> Translates { get; set; }


        public int CreatorId { get; set; }

        public User Creator { get; set; }

        public DateTime CreateDate { get; set; }


        public int? ModifierId { get; set; }

        public User Modifier { get; set; }

        public DateTime? ModifidDate { get; set; }
    }


    //public class PanelBarEntityConfiguration : EntityTypeMap<PanelBar>
    //{
    //    public PanelBarEntityConfiguration()
    //    {
    //        ToTable("PanelBars", "Dictionary");
    //        HasKey(t => t.Id);
    //        Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

    //        HasOptional(t => t.Parent).WithMany(t => t.Children).HasForeignKey(t => t.ParentId).WillCascadeOnDelete(false);
    //        Property(t => t.ParentId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_PanelBar_ParentID")));

    //        Property(t => t.ControllerName).IsUnicode(true).HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_PanelBar_ControllerName_ActionName") { Order = 1 }));
    //        Property(t => t.ActionName).IsUnicode(true).HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_PanelBar_ControllerName_ActionName") { Order = 2 }));
    //        Property(t => t.Url).IsUnicode(true).HasMaxLength(1024);

    //        HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
    //        Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_PanelBar_CreatorId")));
    //        Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


    //        HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
    //        Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_PanelBar_ModifierId")));
    //        Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
    //    }
    //}





    public class PanelBarTranslate
    {
        public int PanelBarId { get; set; }
        public byte CultureId { get; set; }

        public string Text { get; set; }

        public PanelBar PanelBar { get; set; }
    }



    //public class PanelBarTranslateEntityConfiguration : EntityTypeMap<PanelBarTranslate>
    //{
    //    public PanelBarTranslateEntityConfiguration()
    //    {
    //        ToTable("PanelBarTranslates", "Translate");
    //        HasKey(t => new { t.PanelBarId, t.CultureId });

    //        Property(t => t.PanelBarId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_PanelBar_PanelBarId")));
    //        HasRequired(t => t.PanelBar).WithMany(t => t.Translates).HasForeignKey(t => t.PanelBarId).WillCascadeOnDelete(true);

    //        Property(t => t.CultureId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_PanelBar_CultureId")));
    //        HasRequired(t => t.Culture).WithMany().HasForeignKey(t => t.CultureId).WillCascadeOnDelete(false);

    //        Property(t => t.Text).IsRequired().HasMaxLength(400).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_PanelBar_Text")));
    //    }
    //}
}
