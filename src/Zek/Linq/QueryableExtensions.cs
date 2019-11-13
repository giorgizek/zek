using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Zek.Linq
{
    public static class QueryableExtensions
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static readonly MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public static IQueryable<TSource> Equal<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var bound = Expression.Equal(selector.Body, Expression.Constant(value, typeof(TKey)));
            var lambda = Expression.Lambda<Func<TSource, bool>>(bound, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> NotEqual<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var bound = Expression.NotEqual(selector.Body, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TSource, bool>>(bound, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> GreaterThan<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var bound = Expression.GreaterThan(selector.Body, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TSource, bool>>(bound, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> GreaterThanOrEqual<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var bound = Expression.GreaterThanOrEqual(selector.Body, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TSource, bool>>(bound, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> LessThan<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var bound = Expression.LessThan(selector.Body, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TSource, bool>>(bound, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> LessThanOrEqual<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var bound = Expression.LessThanOrEqual(selector.Body, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TSource, bool>>(bound, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey low, TKey high) //where TKey : IComparable<TKey>
        {
            var expression = selector.Body;

            var lowerBound = Expression.GreaterThanOrEqual(expression, Expression.Constant(low));
            var upperBound = Expression.LessThanOrEqual(expression, Expression.Constant(high));

            var lowLambda = Expression.Lambda<Func<TSource, bool>>(lowerBound, selector.Parameters);
            var highLambda = Expression.Lambda<Func<TSource, bool>>(upperBound, selector.Parameters);

            return source.Where(lowLambda).Where(highLambda);
        }
        public static IQueryable<TSource> NotBetween<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey low, TKey high) //where TKey : IComparable<TKey>
        {
            var expression = selector.Body;

            var lowerBound = Expression.LessThan(expression, Expression.Constant(low));
            var upperBound = Expression.GreaterThan(expression, Expression.Constant(high));

            var lowLambda = Expression.Lambda<Func<TSource, bool>>(lowerBound, selector.Parameters);
            var highLambda = Expression.Lambda<Func<TSource, bool>>(upperBound, selector.Parameters);

            return source.Where(lowLambda).Where(highLambda);
        }
        public static IQueryable<TSource> Contains<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var containsBound = Expression.Call(selector.Body, ContainsMethod, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TSource, bool>>(containsBound, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> NotContains<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var containsBound = Expression.Call(selector.Body, ContainsMethod, Expression.Constant(value));
            var notContains = Expression.Not(containsBound);
            var lambda = Expression.Lambda<Func<TSource, bool>>(notContains, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> Begins<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var containsBound = Expression.Call(selector.Body, StartsWithMethod, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TSource, bool>>(containsBound, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> Ends<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey value)
        {
            var containsBound = Expression.Call(selector.Body, EndsWithMethod, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TSource, bool>>(containsBound, selector.Parameters);
            return source.Where(lambda);
        }


        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, TKey key, Expression<Func<TSource, TKey>> lowSelector, Expression<Func<TSource, TKey>> highSelector) where TKey : IComparable<TKey>
        {
            var low = lowSelector.Body;
            var high = highSelector.Body;

            var lowerBound = Expression.LessThanOrEqual(low, Expression.Constant(key));
            var upperBound = Expression.LessThanOrEqual(Expression.Constant(key), high);

            var lowLambda = Expression.Lambda<Func<TSource, bool>>(lowerBound, lowSelector.Parameters);
            var highLambda = Expression.Lambda<Func<TSource, bool>>(upperBound, highSelector.Parameters);

            return source.Where(lowLambda).Where(highLambda);
        }
        public static IQueryable<TSource> Overlap<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> lowSelector, Expression<Func<TSource, TKey>> highSelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            var lowExpression = lowSelector.Body;
            var lowerBound = Expression.GreaterThanOrEqual(lowExpression, Expression.Constant(low));
            var upperBound = Expression.LessThanOrEqual(lowExpression, Expression.Constant(high));
            var predicateBody1 = Expression.And(lowerBound, upperBound);
            var lambda1 = Expression.Lambda<Func<TSource, bool>>(predicateBody1, lowSelector.Parameters);


            var highExpression = highSelector.Body;
            lowerBound = Expression.GreaterThanOrEqual(highExpression, Expression.Constant(low));
            upperBound = Expression.LessThanOrEqual(highExpression, Expression.Constant(high));
            var predicateBody2 = Expression.And(lowerBound, upperBound);
            var lambda2 = Expression.Lambda<Func<TSource, bool>>(predicateBody2, highSelector.Parameters);


            lowerBound = Expression.LessThanOrEqual(lowExpression, Expression.Constant(low));
            upperBound = Expression.GreaterThanOrEqual(highExpression, Expression.Constant(high));
            var predicateBody3 = Expression.And(lowerBound, upperBound);
            var lambda3 = Expression.Lambda<Func<TSource, bool>>(predicateBody3, highSelector.Parameters);

            return source.Where(lambda1.Or(lambda2).Or(lambda3));
        }

        public static IQueryable<TSource> In<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, IEnumerable<TKey> value)
        {
            var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;

            var pe = Expression.Parameter(typeof(TSource));
            var me = Expression.Property(pe, prop.Name);
            var ce = Expression.Constant(value);
            var call = Expression.Call(typeof(Enumerable), "Contains", new[] { me.Type }, ce, me);
            var lambda = Expression.Lambda<Func<TSource, bool>>(call, pe);
            return source.Where(lambda);
        }
    }
}
