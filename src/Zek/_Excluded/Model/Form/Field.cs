using Microsoft.EntityFrameworkCore;

namespace Zek.Model.Form
{
    public class Field : FieldBase
    {
        public int FormId { get; set; }
        public Form Form { get; set; }
    }

    public class FieldMap : FieldBaseMap<Field>
    {
        public FieldMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Document", "Fields");

            HasIndex(x => x.FormId);

            HasOne(x => x.Form).WithMany(x => x.Fields).HasForeignKey(x => x.FormId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
