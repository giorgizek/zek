using Zek.Data;

namespace Zek.Model.DTO.Form
{
    public class FieldDTO : EditBaseDTO
    {
        public string Label { get; set; }
        public string FormatString { get; set; }
        public DataType? DataType { get; set; }
        public ControlType? ControlType { get; set; }
        public bool? Visible { get; set; }
        public int? ColumnIndex { get; set; }
        public int? RowIndex { get; set; }
        public bool? Required { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public int? MaxLength { get; set; }
        public bool? AllowEdit { get; set; }
    }
}
