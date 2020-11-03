using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ITN.TS.DataGrid.Filtering.Text
{
    public class stringNotEqualsFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            if (string.IsNullOrEmpty(Value))
            {
                Expression isNotEmpty = Expression.NotEqual(expression, Expression.Constant(""));
                Expression notNull = Expression.NotEqual(expression, Expression.Constant(null));

                return Expression.AndAlso(notNull, isNotEmpty);
            }

            MethodInfo toUpperMethod = typeof(string).GetMethod("ToUpper", new Type[0]);
            Expression value = Expression.Constant(Value.ToUpper());

            Expression equalsNull = Expression.Equal(expression, Expression.Constant(null));
            Expression toUpper = Expression.Call(expression, toUpperMethod);
            Expression notEquals = Expression.NotEqual(toUpper, value);

            return Expression.OrElse(equalsNull, notEquals);
        }
    }
}
