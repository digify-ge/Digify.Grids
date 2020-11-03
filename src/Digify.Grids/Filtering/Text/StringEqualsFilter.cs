using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Digify.DataGrid.Filtering.Text
{
    public class stringEqualsFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            if (string.IsNullOrEmpty(Value))
            {
                Expression equalsNull = Expression.Equal(expression, Expression.Constant(null));
                Expression isEmpty = Expression.Equal(expression, Expression.Constant(""));

                return Expression.OrElse(equalsNull, isEmpty);
            }

            MethodInfo toUpperMethod = typeof(string).GetMethod("ToUpper", new Type[0]);
            Expression value = Expression.Constant(Value.ToUpper());

            Expression notNull = Expression.NotEqual(expression, Expression.Constant(null));
            Expression toUpper = Expression.Call(expression, toUpperMethod);
            Expression equals = Expression.Equal(toUpper, value);

            return Expression.AndAlso(notNull, equals);
        }
    }
}
