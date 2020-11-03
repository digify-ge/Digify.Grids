using System;
using System.Collections.Generic;
using ITN.TS.DataGrid.Grids;

namespace ITN.TS.DataGrid.Rows
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
