using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Digify.DataGrid.Grids;

namespace Digify.DataGrid.Columns
{
    public class GridColumns<T> : List<IGridColumn<T>>, IGridColumnsOf<T> where T : class
    {
        public IGrid<T> Grid { get; set; }

        public GridColumns(IGrid<T> grid)
        {
            Grid = grid;
        }

        public virtual IGridColumn<T> Add<TValue>(Expression<Func<T, TValue>> expression)
        {
            IGridColumn<T> column = new GridColumn<T, TValue>(Grid, expression);
            Grid.Processors.Add(column);
            Add(column);

            return column;
        }

        public IGridColumn<T> Add<TValue>(string columnName)
        {
            IGridColumn<T> column = new GridColumn<T, TValue>(Grid, columnName);
            Grid.Processors.Add(column);
            Add(column);

            return column;
        }

        public virtual IGridColumn<T> Insert<TValue>(int index, Expression<Func<T, TValue>> expression)
        {
            IGridColumn<T> column = new GridColumn<T, TValue>(Grid, expression);
            Grid.Processors.Add(column);
            Insert(index, column);

            return column;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
