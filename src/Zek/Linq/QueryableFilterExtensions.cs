using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Zek.Data.Filtering;
using Zek.Extensions.Collections;

namespace Zek.Linq
{
    public static class QueryableFilterExtensions
    {
        public static IQueryable<TSource> Filter<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, WhereOperator whereOperator, IList<TKey> value1, IList<TKey> value2 = null, bool filterIfDefault = false)
        {
            if (!filterIfDefault && value1.IsNullOrEmpty())
            {
                return source;
            }

            switch (whereOperator)
            {
                case WhereOperator.In:
                    return source.In(selector, value1);
            }

            return source;
        }
        public static IQueryable<TSource> Filter<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, WhereOperator whereOperator, TKey[] value1, TKey[] value2 = null, bool filterIfDefault = false)
        {
            if (!filterIfDefault && value1.IsNullOrEmpty())
            {
                return source;
            }

            switch (whereOperator)
            {
                case WhereOperator.In:
                    return source.In(selector, value1);
            }

            return source;
        }


        static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }


        public static IQueryable<TSource> Filter<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, WhereOperator whereOperator, TKey value1, TKey value2 = default, bool filterIfDefault = false)
        {
            if (!filterIfDefault && value1 == null)// Equals(value1, default(TKey)))
            {
                return source;
            }

            //if (value1 != null && IsNullableType(selector.Body.Type))
            //{
            //    selector = Expression.Convert(selector, value1.GetType()) as Expression<Func<TSource, TKey>>;
            //}


            switch (whereOperator)
            {
                case WhereOperator.Equals:
                    return source.Equal(selector, value1);

                case WhereOperator.NotEquals:
                    return source.NotEqual(selector, value1);

                case WhereOperator.GreaterThan:
                    return source.GreaterThan(selector, value1);

                case WhereOperator.GreaterThanOrEquals:
                    return source.GreaterThanOrEqual(selector, value1);

                case WhereOperator.LessThan:
                    return source.LessThan(selector, value1);

                case WhereOperator.LessThanOrEquals:
                    return source.LessThanOrEqual(selector, value1);

                case WhereOperator.Between:
                    return source.Between(selector, value1, value2);

                case WhereOperator.Contains:
                    return source.Contains(selector, value1);

                case WhereOperator.NotContains:
                    return source.NotContains(selector, value1);

                case WhereOperator.Begins:
                    return source.Begins(selector, value1);

                case WhereOperator.Ends:
                    return source.Ends(selector, value1);
            }

            return source;
        }


        public static IQueryable<TSource> Filter2<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, WhereOperator whereOperator, TKey value1, TKey value2 = default, bool filterIfDefault = false)
        {
            if (!filterIfDefault && Equals(value1, default(TKey)))
            {
                return source;
            }

            switch (whereOperator)
            {
                case WhereOperator.Equals:
                    return source.Equal(selector, value1);

                case WhereOperator.NotEquals:
                    return source.NotEqual(selector, value1);

                case WhereOperator.GreaterThan:
                    return source.GreaterThan(selector, value1);

                case WhereOperator.GreaterThanOrEquals:
                    return source.GreaterThanOrEqual(selector, value1);

                case WhereOperator.LessThan:
                    return source.LessThan(selector, value1);

                case WhereOperator.LessThanOrEquals:
                    return source.LessThanOrEqual(selector, value1);

                case WhereOperator.Between:
                    return source.Between(selector, value1, value2);

                case WhereOperator.Contains:
                    return source.Contains(selector, value1);

                case WhereOperator.NotContains:
                    return source.NotContains(selector, value1);

                case WhereOperator.Begins:
                    return source.Begins(selector, value1);

                case WhereOperator.Ends:
                    return source.Ends(selector, value1);
            }

            return source;
        }


        //public static IQueryable<TSource> Sort<TSource>(this IQueryable<TSource> source, string sort, string sortOrder)
        //{
        //    return source.OrderBy(x => x);

        //    source.OrderByOrdinal()
        //}

        public static IQueryable<TSource> AutoFilter<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, string>> selector, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return source;
            }

            var whereOperator = WhereOperator.None;
            if (value.StartsWith("*"))
            {
                value = value.Substring(1);
                whereOperator = WhereOperator.Ends;
            }

            if (value.EndsWith("*"))
            {
                value = value.Substring(0, value.Length - 1);
                whereOperator = whereOperator == WhereOperator.Ends
                    ? WhereOperator.Contains
                    : WhereOperator.Begins;
            }

            return source.Filter(selector, whereOperator, value, null, false);
        }

    }
}
