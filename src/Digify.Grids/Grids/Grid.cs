using System.Collections.Generic;
using System.Linq;
using ITN.TS.DataGrid.Columns;
using ITN.TS.DataGrid.Paging;
using ITN.TS.DataGrid.Processors;
using ITN.TS.DataGrid.Rows;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITN.TS.DataGrid.Grids
{
    public class Grid<T> : IGrid<T> where T : class
    {
        public string Name { get; set; }
        public bool IsDataTable { get; set; }
        public string ToolbarId { get; set; }
        public bool IsMultiSelectable { get; set; }
        public string EmptyText { get; set; }
        public string CssClasses { get; set; }

        public IQueryable<T> Source { get; set; }
        public IQueryCollection Query { get; set; }
        public ViewContext ViewContext { get; set; }
        public IList<IGridProcessor<T>> Processors { get; set; }

        IGridColumns<IGridColumn> IGrid.Columns => Columns;
        public IGridColumnsOf<T> Columns { get; set; }

        IGridRows<object> IGrid.Rows => Rows;
        public IGridRowsOf<T> Rows { get; set; }

        IGridPager IGrid.Pager => Pager;
        public bool Ajax { get; set; }
        public string AjaxRoute { get; set; }
        public IGridPager<T> Pager { get; set; }

        public Grid(IEnumerable<T> source)
        {
            Processors = new List<IGridProcessor<T>>();
            Source = source.AsQueryable();

            Name = "Grid";
            IsMultiSelectable = false;
            Columns = new GridColumns<T>(this);
            Rows = new GridRows<T>(this);
        }
    }
}
