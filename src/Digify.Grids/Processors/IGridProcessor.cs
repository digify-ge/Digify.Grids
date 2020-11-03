using System.Linq;

namespace Digify.DataGrid.Processors
{
    public interface IGridProcessor<T>
    {
        GridProcessorType ProcessorType { get; set; }

        IQueryable<T> Process(IQueryable<T> items);
    }
}
