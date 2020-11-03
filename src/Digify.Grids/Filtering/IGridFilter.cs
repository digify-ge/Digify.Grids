using System.Linq.Expressions;

namespace Digify.DataGrid.Filtering
{
    public interface IGridFilter
    {
        string Type { get; set; }
        string Value { get; set; }

        Expression Apply(Expression expression);
    }
}
