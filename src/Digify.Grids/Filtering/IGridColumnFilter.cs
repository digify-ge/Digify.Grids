using Digify.DataGrid.Columns;
using Digify.DataGrid.Processors;

namespace Digify.DataGrid.Filtering
{
    public interface IGridColumnFilter
    {
        string Operator { get; set; }
        IGridFilter First { get; set; }
        IGridFilter Second { get; set; }
    }
    public interface IGridColumnFilter<T> : IGridColumnFilter, IGridProcessor<T>
    {
        IGridColumn<T> Column { get; set; }
    }
}
