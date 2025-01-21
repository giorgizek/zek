using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Form
{
    /// <summary>
    /// Field base
    /// </summary>
    public class FieldBase
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Label { get; set; }
        public string FormatString { get; set; }
        public int ControlType { get; set; }
        public int DataType { get; set; }
        public bool Visible { get; set; }
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        //public string DropDown { get; set; }
        public bool Required { get; set; }
        public bool AllowEdit { get; set; }
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// Field base map
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    public class FieldBaseMap<TField> : EntityTypeMap<TField>
        where TField : FieldBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public FieldBaseMap(ModelBuilder builder) : base(builder)
        {
            Property(x => x.Id).ValueGeneratedOnAdd();
            Property(x => x.Key).HasMaxLength(200);
            Property(x => x.Label).HasMaxLength(400);
            Property(x => x.FormatString).HasMaxLength(200);

            HasIndex(x => x.IsDeleted);
        }
    }
}