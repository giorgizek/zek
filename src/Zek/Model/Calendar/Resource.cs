using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Calendar
{
    [Obsolete]
    public class Resource
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Color { get; set; }
        public int? ImageId { get; set; }
    }


    [Obsolete]
    public class ResourceMap : EntityTypeMap<Resource>
    {
        public ResourceMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Resource", "Calendar");
            HasKey(x => x.Id);
            Property(x => x.Id).ValueGeneratedOnAdd();

            Property(x => x.Name).HasMaxLength(200);
        }
    }
}