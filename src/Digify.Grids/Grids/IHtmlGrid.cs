using System;
using Digify.DataGrid.Columns;
using Digify.DataGrid.Paging;
using Digify.DataGrid.Processors;
using Microsoft.AspNetCore.Html;

namespace Digify.DataGrid.Grids
{
    public interface IHtmlGrid<T> : IHtmlContent
    {
        IGrid<T> Grid { get; }
        string PartialViewName { get; set; }

        IHtmlGrid<T> Build(Action<IGridColumnsOf<T>> builder);
        IHtmlGrid<T> Build(string keyColumn = "");
        IHtmlGrid<T> ProcessWith(IGridProcessor<T> processor);

        IHtmlGrid<T> Filterable(bool isFilterable);
        IHtmlGrid<T> MultiFilterable();
        IHtmlGrid<T> Filterable();

        IHtmlGrid<T> Sortable(bool isSortable);
        IHtmlGrid<T> Sortable();

        IHtmlGrid<T> RowCss(Func<T, string> cssClasses);
        IHtmlGrid<T> Css(string cssClasses);
        IHtmlGrid<T> Empty(string text);
        IHtmlGrid<T> Named(string name);

        IHtmlGrid<T> Pageable(Action<IGridPager<T>> builder);
        IHtmlGrid<T> Pageable();
        IHtmlGrid<T> IsDataTable(bool dataTable);
        IHtmlGrid<T> MultiSelectable(bool isMultiSelectable);
        IHtmlGrid<T> Toolbar(string toolbar);
        IHtmlGrid<T> Ajax(string action);
    }
}
