﻿using System;
using System.Linq.Expressions;

namespace Digify.DataGrid.Filtering.Date
{
    public class DateTimeFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            object value = GetDateValue();
            if (value == null) return null;

            switch (Type)
            {
                case "Equals":
                    return Expression.Equal(expression, Expression.Constant(value, expression.Type));
                case "NotEquals":
                    return Expression.NotEqual(expression, Expression.Constant(value, expression.Type));
                case "LessThan":
                    return Expression.LessThan(expression, Expression.Constant(value, expression.Type));
                case "GreaterThan":
                    return Expression.GreaterThan(expression, Expression.Constant(value, expression.Type));
                case "LessThanOrEqual":
                    return Expression.LessThanOrEqual(expression, Expression.Constant(value, expression.Type));
                case "GreaterThanOrEqual":
                    return Expression.GreaterThanOrEqual(expression, Expression.Constant(value, expression.Type));
                default:
                    return null;
            }
        }

        private object GetDateValue()
        {
            DateTime date;
            if (DateTime.TryParse(Value, out date))
                return date;

            return null;
        }
    }
}
