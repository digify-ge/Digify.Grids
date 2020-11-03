using ITN.TS.DataGrid.Columns;
using ITN.TS.DataGrid.Processors;

namespace ITN.TS.DataGrid.Filtering
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
