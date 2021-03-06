﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Digify.DataGrid.Columns;
using Digify.DataGrid.Processors;

namespace Digify.DataGrid.Filtering
{
    public class GridColumnFilter<T> : IGridColumnFilter<T>
    {
        public string Operator { get; set; }
        public IGridFilter First { get; set; }
        public IGridFilter Second { get; set; }
        public IGridColumn<T> Column { get; set; }
        public GridProcessorType ProcessorType { get; set; }

        public IQueryable<T> Process(IQueryable<T> items)
        {
            Expression expression = CreateFilterExpression();
            if (expression == null)
                return items;

            return items.Where(ToLambda(expression));
        }

        private Expression CreateFilterExpression()
        {
            Expression right = Second?.Apply(Column.Expression.Body);
            Expression left = First?.Apply(Column.Expression.Body);

            if (left != null && right != null)
            {
                switch (Operator)
                {
                    case "And":
                        return Expression.AndAlso(left, right);
                    case "Or":
                        return Expression.OrElse(left, right);
                }
            }

            return left ?? right;
        }
        private Expression<Func<T, bool>> ToLambda(Expression expression)
        {
            return Expression.Lambda<Func<T, bool>>(expression, Column.Expression.Parameters[0]);
        }
    }
}
