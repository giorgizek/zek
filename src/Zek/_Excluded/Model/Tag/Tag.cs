using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Tag
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TagMap : EntityTypeMap<Tag>
    {
        public TagMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Tags", "Tag");
            HasKey(t => t.Id);
            Property(t => t.Id).ValueGeneratedOnAdd();

            Property(t => t.Name).HasMaxLength(400);
            HasIndex(t => t.Name).IsUnique();
        }
    }

}
