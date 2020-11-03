using ITN.TS.DataGrid.Filtering;
using ITN.TS.DataGrid.Processors;

namespace ITN.TS.DataGrid.Columns
{
    public interface IFilterableColumn
    {
        string FilterName { get; set; }
        IGridColumnFilter Filter { get; }
        bool? IsFilterable { get; set; }
        bool? IsMultiFilterable { get; set; }
    }
    public interface IFilterableColumn<T> : IFilterableColumn, IGridProcessor<T>
    {
        new IGridColumnFilter<T> Filter { get; set; }
    }
}
