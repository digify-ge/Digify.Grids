using ITN.TS.DataGrid.Processors;
using ITN.TS.DataGrid.Sorting;

namespace ITN.TS.DataGrid.Columns
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
