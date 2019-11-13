using System.Text;

namespace Zek.Data
{
    public class DbTable
    {
        public DbTable(string tableName = null, string schema = null, string alias = null)
        {
            Schema = schema;
            TableName = tableName;
            Alias = alias;
        }

        public string Schema { get; set; }
        public string TableName { get; set; }
        public string Alias { get; set; }
        public bool? Nolock { get; set; }


        public string ToSql()
        {
            if (string.IsNullOrEmpty(TableName))
                return string.Empty;

            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Schema))
                sb.Append("[" + Schema + "].");

            sb.Append("[" + TableName + "]");

            if (!string.IsNullOrEmpty(Alias))
                sb.Append(" AS " + Alias);

            if (Nolock.GetValueOrDefault(true))
                sb.Append(" (NOLOCK)");


            return sb.ToString();
        }
    }
}
