using System;
using System.Linq.Expressions;

namespace ITN.TS.DataGrid.Filtering.Boolean
{
    public class boolFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            object value = GetboolValue();
            if (value == null) return null;

            return Expression.Equal(expression, Expression.Constant(value, expression.Type));
        }

        private object GetboolValue()
        {
            if (string.Equals(Value, "true", StringComparison.OrdinalIgnoreCase))
                return true;

            if (string.Equals(Value, "false", StringComparison.OrdinalIgnoreCase))
                return false;

            return null;
        }
    }
}
