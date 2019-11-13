using System;
using System.Collections.Generic;
using System.Text;
using Zek.Data.Sql;

namespace Zek.Data
{
    public class SelectStatement
    {
        public SelectStatement(string tableName = null, string schema = null, string alias = null)
        {
            Table = new DbTable(tableName, schema, alias);
        }

        public bool Distinct { get; set; }

        public int? Top { get; set; }
        public bool Percent { get; set; }

        public bool Count { get; set; }

        public DbTable Table { get; set; }

        public List<SelectDbColumn> Columns { get; set; }


        public List<WhereClause> Where { get; set; }


        public List<DbColumn> GroupBy { get; set; }

        public List<DbOrderBy> OrderBy { get; set; }

        public Paging Paging { get; set; }



        public string ToSql()
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(Table?.TableName))
                throw new ArgumentException("TableName is required.", nameof(Table.TableName));

            sb.Append("SELECT");
            if (Count)
            {
                sb.Append(" COUNT(1)");
            }
            else
            {
                if (Distinct)
                    sb.Append(" DISTINCT");
                if (Top != null)
                {
                    sb.AppendFormat(" TOP ").Append("(" + Top.Value.ToString("F0") + ")");
                    if (Percent)
                        sb.Append(" PERCENT");
                }

                if (Columns != null && Columns.Count > 0)
                {
                    foreach (var column in Columns)
                    {
                        sb.AppendLine().Append("\t");

                        if (!string.IsNullOrEmpty(column.Alias))
                            sb.Append(column.Alias + ".");
                        sb.Append(column.ColumnName);
                        if (!string.IsNullOrEmpty(column.AsName))
                            sb.Append(" AS [" + column.AsName + "]");
                        sb.Append(",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                }
                else
                {
                    sb.Append(" *");
                }
            }


            sb.AppendLine();
            sb.Append("FROM ");

            sb.Append(Table.ToSql());


            if (Where != null && Where.Count > 0)
            {
                var whereAnd = "WHERE";
                foreach (var where in Where)
                {
                    var sql = SqlFilterHelper.GetWhereClause(where.ToSql(), where.Operator, !where.FilterEmpty, where.Value1, where.Value2);
                    if (!string.IsNullOrEmpty(sql))
                    {
                        sb.AppendLine().Append(whereAnd + " " + sql);
                        whereAnd = "AND";
                    }
                }
            }

            if (GroupBy != null && GroupBy.Count > 0)
            {
                sb.AppendLine();
                sb.Append("GROUP BY ");
                foreach (var gb in GroupBy)
                {
                    sb.Append(gb.ToSql() + ", ");
                }
                sb.Remove(sb.Length - 2, 2);
            }

            if (OrderBy != null && OrderBy.Count > 0 && !Count)
            {
                sb.AppendLine();
                sb.Append("ORDER BY ");
                foreach (var ob in OrderBy)
                {
                    if (!string.IsNullOrEmpty(ob.Alias))
                        sb.Append(ob.Alias + ".");
                    sb.Append("[" + ob.ColumnName + "]");
                    if (ob.Asc != null)

                        sb.Append(ob.Asc.Value ? " ASC" : " DESC");

                    if (!string.IsNullOrEmpty(ob.CollationName))
                        sb.Append(" COLLATE " + ob.CollationName);

                    sb.Append(", ");
                }
                sb.Remove(sb.Length - 2, 2);
            }

            if (Paging != null && !Count)
            {
                if (OrderBy == null || OrderBy.Count == 0)
                {
                    sb.AppendLine();
                    sb.Append("ORDER BY 1");
                }

                sb.AppendLine();
                sb.Append(Paging.ToSql());
            }


            return sb.ToString();
        }
    }


    public class Paging
    {
        public Paging(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        private int _pageNumber;
        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                if (value != _pageNumber)
                {
                    if (value < 1)
                        throw new ArgumentOutOfRangeException(nameof(PageNumber), "PageNumber cannot be below 1.");
                    _pageNumber = value;
                }
            }
        }

        private int? _pageSize;

        public int? PageSize
        {
            get => _pageSize;
            set
            {
                if (value != _pageSize)
                {
                    if (value < 1)
                        throw new ArgumentOutOfRangeException(nameof(PageSize), "PageSize cannot be less than 1.");
                    _pageSize = value;
                }
            }
        }


        public string ToSql()
        {
            return $"OFFSET {(PageNumber - 1) * PageSize} ROWS FETCH NEXT {PageSize} ROWS ONLY";
        }
    }
}
