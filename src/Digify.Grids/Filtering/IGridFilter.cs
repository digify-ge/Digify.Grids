using System.Linq.Expressions;

namespace ITN.TS.DataGrid.Filtering
{
    public interface IGridFilter
    {
        string Type { get; set; }
        string Value { get; set; }

        Expression Apply(Expression expression);
    }
}
