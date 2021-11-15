using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Zek.Extensions.Collections;

namespace Zek.Linq
{
    public static class QueryableExtensions
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static readonly MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        private static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }


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
            Expression e1 = selector.Body;
            Expression e2 = Expression.Constant(value);

            if (IsNullableType(selector.Body.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, selector.Body.Type);
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
                e1 = Expression.Convert(e1, e2.Type);

            var bound = Expression.GreaterThanOrEqual(e1, e2);
            //var bound = Expression.GreaterThanOrEqual(selector.Body, Expression.Constant(value));
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
            Expression e1 = selector.Body;
            Expression e2 = Expression.Constant(value);

            if (IsNullableType(selector.Body.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, selector.Body.Type);
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
                e1 = Expression.Convert(e1, e2.Type);

            var bound = Expression.LessThanOrEqual(e1, e2);
            //var bound = Expression.LessThanOrEqual(selector.Body, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TSource, bool>>(bound, selector.Parameters);
            return source.Where(lambda);
        }
        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> selector, TKey low, TKey high) //where TKey : IComparable<TKey>
        {
            if (low != null)
                source = GreaterThanOrEqual(source, selector, low);

            if (high != null)
                source = LessThanOrEqual(source, selector, high);

            return source;
         /*    var expression = selector.Body;

          Expression lowExpression = Expression.Constant(low);
            if (IsNullableType(selector.Body.Type) && !IsNullableType(lowExpression.Type))
                lowExpression = Expression.Convert(lowExpression, selector.Body.Type);
            else if (!IsNullableType(expression.Type) && IsNullableType(lowExpression.Type))
                expression = Expression.Convert(expression, lowExpression.Type);
            var lowerBound = Expression.GreaterThanOrEqual(expression, lowExpression);

            Expression upperExpression = Expression.Constant(high);
            if (IsNullableType(selector.Body.Type) && !IsNullableType(upperExpression.Type))
                upperExpression = Expression.Convert(upperExpression, selector.Body.Type);
            else if (!IsNullableType(expression.Type) && IsNullableType(upperExpression.Type))
                expression = Expression.Convert(expression, upperExpression.Type);
            var upperBound = Expression.LessThanOrEqual(expression, upperExpression);

            //var lowerBound = Expression.GreaterThanOrEqual(expression, Expression.Constant(low));
            //var upperBound = Expression.LessThanOrEqual(expression, Expression.Constant(high));
            var lowLambda = Expression.Lambda<Func<TSource, bool>>(lowerBound, selector.Parameters);
            var highLambda = Expression.Lambda<Func<TSource, bool>>(upperBound, selector.Parameters);

            return source.Where(lowLambda).Where(highLambda);*/
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
        public static IQueryable<TSource> ContainsAny<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, string>> selector, string value)
        {
            if (string.IsNullOrEmpty(value))
                return source;

            var parts = value.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                foreach (var part in parts)
                {
                    source = source.Contains(selector, part);
                }
            }

            return source;
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
        public static IQueryable<TSource> Overlaps<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> lowSelector, Expression<Func<TSource, TKey>> highSelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            var lowExpression = lowSelector.Body;
            var lowerBound = Expression.GreaterThan(lowExpression, Expression.Constant(low));
            var upperBound = Expression.LessThan(lowExpression, Expression.Constant(high));
            var predicateBody1 = Expression.And(lowerBound, upperBound);
            var lambda1 = Expression.Lambda<Func<TSource, bool>>(predicateBody1, lowSelector.Parameters);


            var highExpression = highSelector.Body;
            lowerBound = Expression.GreaterThan(highExpression, Expression.Constant(low));
            upperBound = Expression.LessThan(highExpression, Expression.Constant(high));
            var predicateBody2 = Expression.And(lowerBound, upperBound);
            var lambda2 = Expression.Lambda<Func<TSource, bool>>(predicateBody2, highSelector.Parameters);


            lowerBound = Expression.LessThanOrEqual(lowExpression, Expression.Constant(low));
            upperBound = Expression.GreaterThanOrEqual(highExpression, Expression.Constant(high));
            var predicateBody3 = Expression.And(lowerBound, upperBound);
            var lambda3 = Expression.Lambda<Func<TSource, bool>>(predicateBody3, highSelector.Parameters);

            return source.Where(lambda1.Or(lambda2).Or(lambda3));
        }
        public static IQueryable<TSource> Intersects<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> lowSelector, Expression<Func<TSource, TKey>> highSelector, TKey low, TKey high) where TKey : IComparable<TKey>
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

        [Obsolete("User Intersects method instead")]
        public static IQueryable<TSource> Overlap<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> lowSelector, Expression<Func<TSource, TKey>> highSelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            return Overlaps(source, lowSelector, highSelector, low, high);
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
