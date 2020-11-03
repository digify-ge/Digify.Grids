using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ITN.TS.DataGrid.Grids;

namespace ITN.TS.DataGrid.Columns
{
    public interface IGridColumns<out T> : IEnumerable<T> where T : IGridColumn
    {
    }

    public interface IGridColumnsOf<T> : IGridColumns<IGridColumn<T>>
    {
        IGrid<T> Grid { get; set; }

        IGridColumn<T> Add<TValue>(Expression<Func<T, TValue>> constraint);
        IGridColumn<T> Add<TValue>(string columnName);
        IGridColumn<T> Insert<TValue>(int index, Expression<Func<T, TValue>> constraint);
    }
}
