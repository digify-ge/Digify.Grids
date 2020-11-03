using System;
using System.Linq;
using System.Linq.Expressions;
using ITN.TS.DataGrid.Filtering;
using ITN.TS.DataGrid.Grids;
using ITN.TS.DataGrid.Processors;
using ITN.TS.DataGrid.Rows;
using ITN.TS.DataGrid.Sorting;
using Microsoft.AspNetCore.Html;

namespace ITN.TS.DataGrid.Columns
{
    public abstract class BaseGridColumn<T, TValue> : IGridColumn<T> where T : class
    {
        public string Name { get; set; }
        public string Format { get; set; }
        public Func<T, string> FormatExpression { get; set; }
        public Func<T, string> RouteValues { get; set; }
        public string CssClasses { get; set; }
        public bool IsEncoded { get; set; }
        public IHtmlContent Title { get; set; }

        public bool? IsSortable { get; set; }
        public bool IsKey { get; set; } = false;
        public GridSortOrder? FirstSortOrder { get; set; }
        public GridSortOrder? InitialSortOrder { get; set; }
        public virtual GridSortOrder? SortOrder { get; set; }

        public string FilterName { get; set; }
        public bool? IsFilterable { get; set; }
        public bool? IsMultiFilterable { get; set; }
        IGridColumnFilter IFilterableColumn.Filter => Filter;
        public virtual IGridColumnFilter<T> Filter { get; set; }

        public IGrid<T> Grid { get; set; }
        public Func<T, object> RenderValue { get; set; }
        public GridProcessorType ProcessorType { get; set; }
        public Func<T, TValue> ExpressionValue { get; set; }
        LambdaExpression IGridColumn<T>.Expression => Expression;
        public Expression<Func<T, TValue>> Expression { get; set; }

        public virtual IGridColumn<T> RenderedAs(Func<T, object> value)
        {
            RenderValue = value;

            return this;
        }

        public virtual IGridColumn<T> MultiFilterable(bool isMultiple)
        {
            IsMultiFilterable = isMultiple;

            return this;
        }
        public virtual IGridColumn<T> Filterable(bool isFilterable)
        {
            IsFilterable = isFilterable;

            return this;
        }
        public virtual IGridColumn<T> FilteredAs(string filterName)
        {
            FilterName = filterName;

            return this;
        }

        public virtual IGridColumn<T> InitialSort(GridSortOrder order)
        {
            InitialSortOrder = order;

            return this;
        }
        public virtual IGridColumn<T> FirstSort(GridSortOrder order)
        {
            FirstSortOrder = order;

            return this;
        }
        public virtual IGridColumn<T> Sortable(bool isSortable)
        {
            IsSortable = isSortable;

            return this;
        }

        public IGridColumn<T> Key(bool isKey = true)
        {
            IsKey = isKey;
            return this;
        }

        public virtual IGridColumn<T> Encoded(bool isEncoded)
        {
            IsEncoded = isEncoded;

            return this;
        }
        public virtual IGridColumn<T> Formatted(string format)
        {
            Format = format;

            return this;
        }
        public virtual IGridColumn<T> Formatted(Expression<Func<T, string>> format)
        {
            FormatExpression =  format.Compile();
            return this;
        }
        public virtual IGridColumn<T> SetRoute(Expression<Func<T, string>> format)
        {
            RouteValues = format.Compile();
            return this;
        }
        public virtual IGridColumn<T> Css(string cssClasses)
        {
            CssClasses = cssClasses;

            return this;
        }
        public virtual IGridColumn<T> Titled(object value)
        {
            Title = value as IHtmlContent ?? new HtmlString(value?.ToString());

            return this;
        }
        public virtual IGridColumn<T> Named(string name)
        {
            Name = name;

            return this;
        }

        public abstract IQueryable<T> Process(IQueryable<T> items);
        public abstract IHtmlContent ValueFor(IGridRow<object> row);
        public abstract IHtmlContent ValueForKey(IGridRow<object> row);
    }
}
