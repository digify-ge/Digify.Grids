using System;
using Digify.DataGrid.Grids;
using Digify.DataGrid.Processors;

namespace Digify.DataGrid.Paging
{
    public interface IGridPager
    {
        int TotalRows { get; }
        int TotalPages { get; }

        int CurrentPage { get; set; }
        int RowsPerPage { get; set; }

        int FirstDisplayPage { get; }
        int PagesToDisplay { get; set; }

        string CssClasses { get; set; }
        string PartialViewName { get; set; }
    }

    public interface IGridPager<T> : IGridProcessor<T>, IGridPager
    {
        IGrid<T> Grid { get; }
    }
}
