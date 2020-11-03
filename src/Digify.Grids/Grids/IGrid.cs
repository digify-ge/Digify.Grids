using System.Collections.Generic;
using System.Linq;
using Digify.DataGrid.Columns;
using Digify.DataGrid.Paging;
using Digify.DataGrid.Processors;
using Digify.DataGrid.Rows;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Digify.DataGrid.Grids
{
    public interface IGrid
    {
        string Name { get; set; }
        bool IsDataTable { get; set; }
        string ToolbarId { get; set; }
        bool IsMultiSelectable { get; set; }
        string EmptyText { get; set; }
        string CssClasses { get; set; }

        ViewContext ViewContext { get; set; }
        IQueryCollection Query { get; set; }

        IGridColumns<IGridColumn> Columns { get; }

        IGridRows<object> Rows { get; }

        IGridPager Pager { get; }
        bool Ajax { get; set; }
        string AjaxRoute { get; set; }
    }

    public interface IGrid<T> : IGrid
    {
        IList<IGridProcessor<T>> Processors { get; set; }
        IQueryable<T> Source { get; set; }

        new IGridColumnsOf<T> Columns { get; }
        new IGridRowsOf<T> Rows { get; }

        new IGridPager<T> Pager { get; set; }
        
    }
}
