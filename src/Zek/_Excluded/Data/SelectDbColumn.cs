namespace Zek.Data
{
    public class SelectDbColumn : DbColumn
    {
        public SelectDbColumn(string columnName = null, string alias = null, string asName = null) : base(columnName, alias)
        {
            AsName = asName;
        }
        public string AsName { get; set; }
    }
}