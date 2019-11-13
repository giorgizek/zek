using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class Category : BaseModel<int>
    {
        public int? ParentId { get; set; }
        public Category Parent { get; set; }

        public List<Category> Children { get; set; }
    }

    public class CategoryMap : BaseModelMap<Category, int>
    {
        public CategoryMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Categories", "Dictionary");

            HasOne(t => t.Parent).WithMany(t => t.Children).HasForeignKey(t => t.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class CategoryTranslate : TranslateModel<Category, int>
    {
    }

    public class CategoryTranslateMap : TranslateModelMap<CategoryTranslate, Category, int>
    {
        public CategoryTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("CategoryTranslates", "Translate");
        }
    }
}
