using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Digify.DataGrid.Filtering.Text
{
    public class stringEndsWithFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
            MethodInfo toUpperMethod = typeof(string).GetMethod("ToUpper", new Type[0]);
            Expression value = Expression.Constant(Value.ToUpper());

            Expression notNull = Expression.NotEqual(expression, Expression.Constant(null));
            Expression toUpper = Expression.Call(expression, toUpperMethod);

            Expression endsWith = Expression.Call(toUpper, endsWithMethod, value);

            return Expression.AndAlso(notNull, endsWith);
        }
    }
}
