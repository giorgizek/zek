namespace Zek.Data
{
    public class DbOrderBy : DbColumn
    {
        public DbOrderBy(string columnName = null, string alias = null, bool? asc = null) : base(columnName, alias)
        {
            Asc = asc;
        }

        public bool? Asc { get; set; }

        public string CollationName { get; set; }
    }
}