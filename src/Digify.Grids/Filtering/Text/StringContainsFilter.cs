using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Digify.DataGrid.Filtering.Text
{
    public class stringContainsFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            MethodInfo toUpperMethod = typeof(string).GetMethod("ToUpper", new Type[0]);
            MethodInfo containsMethod = typeof(string).GetMethod("Contains");
            Expression value = Expression.Constant(Value.ToUpper());

            Expression notNull = Expression.NotEqual(expression, Expression.Constant(null));
            Expression toUpper = Expression.Call(expression, toUpperMethod);

            Expression contains = Expression.Call(toUpper, containsMethod, value);

            return Expression.AndAlso(notNull, contains);
        }
    }
}
