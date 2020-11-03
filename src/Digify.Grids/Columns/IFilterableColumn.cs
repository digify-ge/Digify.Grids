using Digify.DataGrid.Filtering;
using Digify.DataGrid.Processors;

namespace Digify.DataGrid.Columns
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
