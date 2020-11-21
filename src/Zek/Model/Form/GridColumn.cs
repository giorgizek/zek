using Microsoft.EntityFrameworkCore;

namespace Zek.Model.Form
{
    public class Column : FieldBase
    {
        public int FieldId { get; set; }
        public Field Field { get; set; }
    }

    public class ColumnMap<TColumn> : FieldBaseMap<TColumn>
        where TColumn : Column
    {
        public ColumnMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Form", "Columns");
            HasIndex(x => x.FieldId);
        }
    }
}
