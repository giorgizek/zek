using Zek.Extensions;

namespace Zek.Data
{
    public class DbColumn
    {
        public DbColumn(string columnName = null, string alias = null)
        {
            ColumnName = columnName;
            Alias = alias;
        }

        public string Alias { get; set; }
        public string ColumnName { get; set; }


        public string ToSql()
        {
            return string.IsNullOrEmpty(ColumnName)
                ? string.Empty
                : Alias.Add(".", "[" + ColumnName + "]");
        }
    }
}