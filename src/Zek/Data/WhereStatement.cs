using Zek.Data.Filtering;

namespace Zek.Data
{
    public class WhereClause : DbColumn
    {
        public WhereClause(string columnName = null, WhereOperator op = WhereOperator.None, bool filterEmpty = false, object value1 = null, object value2 = null) : this(columnName, null, op, filterEmpty, value1, value2)
        {
        }
        public WhereClause(string columnName = null, string alias = null, WhereOperator op = WhereOperator.None, bool filterEmpty = false, object value1 = null, object value2 = null) : base(columnName, alias)
        {
            Operator = op;
            FilterEmpty = filterEmpty;
            Value1 = value1;
            Value2 = value2;
        }

        public DbColumn Column { get; set; }

        public WhereOperator Operator { get; set; }

        public bool FilterEmpty { get; set; }

        public object Value1 { get; set; }

        public object Value2 { get; set; }
    }

    //public class WhereStatement
    //{
    //    public WhereStatement(bool addWhereText, GroupOperator groupOperator = GroupOperator.And)
    //    {
    //        AddWhereText = addWhereText;
    //        GroupOperator = groupOperator;
    //    }

    //    public bool AddWhereText { get; set; }
    //    public GroupOperator GroupOperator { get; set; }


    //    public int Count => WhereStatement.Count;

    //    public bool IsEmpty => WhereStatement.Count == 0;


    //    private List<string> _whereStatement = new List<string>();
    //    public List<string> WhereStatement
    //    {
    //        get { return _whereStatement; }
    //        set
    //        {
    //            if (value == null) value = new List<string>();
    //            _whereStatement = value;
    //        }
    //    }



    //    public void AddWhere(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, object value1, object value2)
    //    {
    //        Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value1, value2));
    //    }
    //    public void AddWhere(string fieldName1, string fieldName2, WhereOperator whereOperator, bool doNotFilterIfEmpty, object value1, object value2)
    //    {
    //        Add(SqlFilterHelper.GetWhereClause(fieldName1, fieldName2, whereOperator, doNotFilterIfEmpty, value1, value2));
    //    }

    //    public void AddWhere(string fieldName, string whereOperator, bool doNotFilterIfEmpty, object value)
    //    {
    //        Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value));
    //    }
    //    public void AddWhere(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, object value)
    //    {
    //        Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value));
    //    }

    //    public void Add(string whereClause)
    //    {
    //        if (string.IsNullOrWhiteSpace(whereClause) || _whereStatement.Contains(whereClause)) return;

    //        _whereStatement.Add(whereClause);
    //    }

    //    public override string ToString()
    //    {
    //        return (AddWhereText && _whereStatement.Count > 0 ? "WHERE " : string.Empty) + string.Join(GroupOperator == GroupOperator.And ? " AND " : " OR ", _whereStatement.ToArray());
    //    }
    //}
}
