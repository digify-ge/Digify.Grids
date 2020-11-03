using System;
using ITN.TS.DataGrid.Columns;

namespace ITN.TS.DataGrid.Filtering
{
    public interface IGridFilters
    {
        IGridColumnFilter<T> GetFilter<T>(IGridColumn<T> column);

        void Register(Type forType, string filterType, Type filter);
        void Unregister(Type forType, string filterType);
    }
}
