using System;
using Digify.DataGrid.Columns;

namespace Digify.DataGrid.Filtering
{
    public interface IGridFilters
    {
        IGridColumnFilter<T> GetFilter<T>(IGridColumn<T> column);

        void Register(Type forType, string filterType, Type filter);
        void Unregister(Type forType, string filterType);
    }
}
