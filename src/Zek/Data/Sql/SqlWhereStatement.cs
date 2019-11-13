using System;
using System.Collections.Generic;
using Zek.Data.Filtering;

namespace Zek.Data.Sql
{


    public class SqlWhereStatement
    {
        public SqlWhereStatement(bool addWhereText, GroupOperator groupOperator = GroupOperator.And)
        {
            AddWhereText = addWhereText;
            GroupOperator = groupOperator;
        }

        public bool AddWhereText { get; set; }
        public GroupOperator GroupOperator { get; set; }

        public int Count => WhereStatement.Count;

        public bool IsEmpty => WhereStatement.Count == 0;

        private List<string> _whereStatement = new List<string>();
        public List<string> WhereStatement
        {
            get { return _whereStatement; }
            set
            {
                if (value == null) value = new List<string>();
                _whereStatement = value;
            }
        }

        public void AddWhere(string fieldName, string whereOperator, bool doNotFilterIfEmpty, DateTime? value, DateTimePrecision precission)
        {
            Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value, precission));
        }
        public void AddWhere(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, DateTime? value, DateTimePrecision precission)
        {
            Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value, precission));
        }

        public void AddWhere(string fieldName, string whereOperator, bool doNotFilterIfEmpty, DateTime? value1, DateTime? value2, DateTimePrecision precission)
        {
            Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value1, value2, precission));
        }
        public void AddWhere(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, DateTime? value1, DateTime? value2, DateTimePrecision precission)
        {
            Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value1, value2, precission));
        }


        public void AddWhere(string fieldName, string whereOperator, bool doNotFilterIfEmpty, object value1, object value2)
        {
            Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value1, value2));
        }
        public void AddWhere(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, object value1, object value2)
        {
            Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value1, value2));
        }
        public void AddWhere(string fieldName1, string fieldName2, WhereOperator whereOperator, bool doNotFilterIfEmpty, object value1, object value2)
        {
            Add(SqlFilterHelper.GetWhereClause(fieldName1, fieldName2, whereOperator, doNotFilterIfEmpty, value1, value2));
        }

        public void AddWhere(string fieldName, string whereOperator, bool doNotFilterIfEmpty, object value)
        {
            Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value));
        }
        public void AddWhere(string fieldName, WhereOperator whereOperator, bool doNotFilterIfEmpty, object value)
        {
            Add(SqlFilterHelper.GetWhereClause(fieldName, whereOperator, doNotFilterIfEmpty, value));
        }

        public void Add(string whereClause)
        {
            if (string.IsNullOrWhiteSpace(whereClause) || _whereStatement.Contains(whereClause)) return;

            _whereStatement.Add(whereClause);
        }

        public override string ToString()
        {
            return (AddWhereText && _whereStatement.Count > 0 ? "WHERE " : string.Empty) + string.Join(GroupOperator == GroupOperator.And ? " AND " : " OR ", _whereStatement.ToArray());
        }
    }
}
