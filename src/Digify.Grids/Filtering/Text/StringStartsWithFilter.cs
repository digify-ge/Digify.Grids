using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ITN.TS.DataGrid.Filtering.Text
{
    public class stringStartsWithFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            MethodInfo toUpperMethod = typeof(string).GetMethod("ToUpper", new Type[0]);
            Expression value = Expression.Constant(Value.ToUpper());

            Expression notNull = Expression.NotEqual(expression, Expression.Constant(null));
            Expression toUpper = Expression.Call(expression, toUpperMethod);

            Expression startsWith = Expression.Call(toUpper, startsWithMethod, value);

            return Expression.AndAlso(notNull, startsWith);
        }
    }
}
