using Digify.DataGrid.Processors;
using Digify.DataGrid.Sorting;

namespace Digify.DataGrid.Columns
{
    public interface ISortableColumn
    {
        bool? IsSortable { get; set; }
        GridSortOrder? SortOrder { get; set; }
        GridSortOrder? FirstSortOrder { get; set; }
        GridSortOrder? InitialSortOrder { get; set; }
    }

    public interface ISortableColumn<T> : IGridProcessor<T>
    {
    }
}
