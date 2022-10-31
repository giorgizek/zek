/*using System;
using System.Linq.Expressions;
using Zek.Utils;

namespace Zek.Web
{
    public static class DisplayNameExtensions
    {
        public static string GetDisplayName<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var me = expression.Body as MemberExpression;
            if (me == null)
                throw new ArgumentException("Must be a MemberExpression.", nameof(expression));

            return DisplayNameHelper.GetDisplayNameForProperty(me.Member);
        }
    }
}*/