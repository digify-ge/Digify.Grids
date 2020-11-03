using System;
using System.Linq.Expressions;
using Digify.DataGrid.Grids;
using Digify.DataGrid.Rows;
using Digify.DataGrid.Sorting;
using Microsoft.AspNetCore.Html;

namespace Digify.DataGrid.Columns
{
    public interface IGridColumn : IFilterableColumn, ISortableColumn
    {
        string Name { get; set; }
        string Format { get; set; }
        string CssClasses { get; set; }
        bool IsEncoded { get; set; }
        bool IsKey { get; set; }
        IHtmlContent Title { get; set; }

        IHtmlContent ValueFor(IGridRow<object> row);
        IHtmlContent ValueForKey(IGridRow<object> row);
    }

    public interface IGridColumn<T> : IFilterableColumn<T>, ISortableColumn<T>, IGridColumn
    {
        IGrid<T> Grid { get; }
        LambdaExpression Expression { get; }

        IGridColumn<T> RenderedAs(Func<T, object> value);

        IGridColumn<T> MultiFilterable(bool isMultiple);
        IGridColumn<T> Filterable(bool isFilterable);
        IGridColumn<T> FilteredAs(string filterName);

        IGridColumn<T> InitialSort(GridSortOrder order);
        IGridColumn<T> FirstSort(GridSortOrder order);
        IGridColumn<T> Sortable(bool isSortable);
        IGridColumn<T> Key(bool isKey = true);


        IGridColumn<T> Encoded(bool isEncoded);
        IGridColumn<T> Formatted(string format);
        IGridColumn<T> Formatted(Expression<Func<T, string>> format);
        IGridColumn<T> SetRoute(Expression<Func<T, string>> routeValues);
        IGridColumn<T> Css(string cssClasses);
        IGridColumn<T> Titled(object title);
        IGridColumn<T> Named(string name);
    }
}
