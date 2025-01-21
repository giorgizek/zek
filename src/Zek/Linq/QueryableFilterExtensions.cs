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

            return whereOperator switch
            {
                WhereOperator.In => source.In(selector, value1),
                _ => source,
            };
        }
        public static IQueryable<TSource> Filter<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, WhereOperator whereOperator, TKey[] value1, TKey[] value2 = null, bool filterIfDefault = false)
        {
            if (!filterIfDefault && value1.IsNullOrEmpty())
            {
                return source;
            }

            return whereOperator switch
            {
                WhereOperator.In => source.In(selector, value1),
                _ => source,
            };
        }





        public static IQueryable<TSource> Filter<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, WhereOperator whereOperator, TKey value1, TKey value2 = default, bool filterIfDefault = false)
        {
            if (!filterIfDefault && value1 is null)// Equals(value1, default(TKey)))
            {
                if (value2 is null || whereOperator != WhereOperator.Between)
                    return source;
            }

            //if (value1 != null && IsNullableType(selector.Body.Type))
            //{
            //    selector = Expression.Convert(selector, value1.GetType()) as Expression<Func<TSource, TKey>>;
            //}



            return whereOperator switch
            {
                WhereOperator.Equals => source.Equal(selector, value1),
                WhereOperator.NotEquals => source.NotEqual(selector, value1),
                WhereOperator.GreaterThan => source.GreaterThan(selector, value1),
                WhereOperator.GreaterThanOrEquals => source.GreaterThanOrEqual(selector, value1),
                WhereOperator.LessThan => source.LessThan(selector, value1),
                WhereOperator.LessThanOrEquals => source.LessThanOrEqual(selector, value1),
                WhereOperator.Between => source.Between(selector, value1, value2),
                WhereOperator.Contains => source.Contains(selector, value1),
                WhereOperator.ContainsAny => source.ContainsAny(selector as Expression<Func<TSource, string>>, value1 as string),
                WhereOperator.NotContains => source.NotContains(selector, value1),
                WhereOperator.Begins => source.Begins(selector, value1),
                WhereOperator.Ends => source.Ends(selector, value1),
                _ => source,
            };
        }


        public static IQueryable<TSource> Filter2<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, WhereOperator whereOperator, TKey value1, TKey value2 = default, bool filterIfDefault = false)
        {
            if (!filterIfDefault && Equals(value1, default(TKey)))
            {
                return source;
            }

            return whereOperator switch
            {
                WhereOperator.Equals => source.Equal(selector, value1),
                WhereOperator.NotEquals => source.NotEqual(selector, value1),
                WhereOperator.GreaterThan => source.GreaterThan(selector, value1),
                WhereOperator.GreaterThanOrEquals => source.GreaterThanOrEqual(selector, value1),
                WhereOperator.LessThan => source.LessThan(selector, value1),
                WhereOperator.LessThanOrEquals => source.LessThanOrEqual(selector, value1),
                WhereOperator.Between => source.Between(selector, value1, value2),
                WhereOperator.Contains => source.Contains(selector, value1),
                WhereOperator.NotContains => source.NotContains(selector, value1),
                WhereOperator.Begins => source.Begins(selector, value1),
                WhereOperator.Ends => source.Ends(selector, value1),
                _ => source,
            };
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
