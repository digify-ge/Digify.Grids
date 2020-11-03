using System.Linq.Expressions;

namespace Digify.DataGrid.Filtering
{
    public abstract class BaseGridFilter : IGridFilter
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public abstract Expression Apply(Expression expression);
    }
}
