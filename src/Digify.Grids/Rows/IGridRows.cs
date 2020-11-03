using System;
using System.Collections.Generic;
using Digify.DataGrid.Grids;

namespace Digify.DataGrid.Rows
{
    public interface IGridRows<out T> : IEnumerable<IGridRow<T>>
    {
    }

    public interface IGridRowsOf<T> : IGridRows<T>
    {
        Func<T, string> CssClasses { get; set; }
        IGrid<T> Grid { get; }
    }
}
