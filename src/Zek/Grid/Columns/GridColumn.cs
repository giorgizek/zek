using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Grid.Columns
{
    public class GridColumn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FormatString { get; set; }
        public int FormatType { get; set; }
        public bool Visible { get; set; }
        public int VisibleIndex { get; set; }
        public bool Required { get; set; }
        public bool AllowEdit { get; set; }
    }

    public class GridColumnBaseMap : EntityTypeMap<GridColumn>
    {
        public GridColumnBaseMap(ModelBuilder builder) : base(builder)
        {
            Property(x => x.Id).ValueGeneratedOnAdd();
            Property(x => x.Name).HasMaxLength(200);
            Property(x => x.FormatString).HasMaxLength(200);
        }
    }
}
