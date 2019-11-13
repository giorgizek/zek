using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Zek.Data.Entity;
using Zek.Model.Base;

namespace Zek.Model.Faq
{
    public class Faq : PocoModel<int>
    {
        public int ApplicationId { get; set; }
        public int AreaId { get; set; }

        public string Name { get; set; }

        public List<FaqTranslate> FaqTranslates { get; set; }
    }
    

    public class FaqMap : PocoModelMap<Faq, int>
    {
        public FaqMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Faqs", "Dictionary");
            HasIndex(f => new { f.ApplicationId, f.AreaId });

            Property(f => f.Name).HasMaxLength(400).IsRequired();
            HasIndex(t => t.Name);
        }
    }



    public class FaqTranslate
    {
        public int Id { get; set; }
        public Faq Faq { get; set; }

        public byte CultureId { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }

        public string QuestionHtml { get; set; }
        public string AnswerHtml { get; set; }
    }

    public class FaqTranslateMap : EntityTypeMap<FaqTranslate>
    {
        public FaqTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("FaqTranslates", "Translate");
            HasKey(t => new { t.Id, t.CultureId });

            HasOne(t => t.Faq).WithMany(t => t.FaqTranslates).HasForeignKey(t => t.Id).OnDelete(DeleteBehavior.Cascade);
            HasIndex(t => t.CultureId);

            Property(t => t.Question).IsRequired().HasMaxLength(400);
            HasIndex(t => t.Question);

        }
    }
}
