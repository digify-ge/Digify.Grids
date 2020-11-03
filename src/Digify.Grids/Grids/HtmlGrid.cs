using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using ITN.TS.DataGrid.Columns;
using ITN.TS.DataGrid.Paging;
using ITN.TS.DataGrid.Processors;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Linq;

namespace ITN.TS.DataGrid.Grids
{
    public class HtmlGrid<T> : IHtmlGrid<T> 
    {
        public IGrid<T> Grid { get; set; }
        public IHtmlHelper Html { get; set; }
        public string PartialViewName { get; set; }

        public HtmlGrid(IHtmlHelper html, IGrid<T> grid, string partialName)
        {
            grid.Query = grid.Query ?? html.ViewContext.HttpContext.Request.Query;
            grid.ViewContext = grid.ViewContext ?? html.ViewContext;
            PartialViewName = partialName;
            Html = html;
            Grid = grid;
        }
        public virtual IHtmlGrid<T> Build(Action<IGridColumnsOf<T>> builder)
        {
            builder(Grid.Columns);

            return this;
        }

        public IHtmlGrid<T> Build(string keyColumn = "")
        {
            var lProps = (Grid.Source.Any() ? Grid.Source.First().GetType().GetProperties() : typeof(T).GetProperties()).ToList();
            lProps.ForEach(e =>
            {
                Grid.Columns.Add<T>(e.Name).Key(!string.IsNullOrEmpty(keyColumn) && keyColumn.ToLower().Equals(e.Name.ToLower()));
            });

            return this;
        }

        public virtual IHtmlGrid<T> ProcessWith(IGridProcessor<T> processor)
        {
            Grid.Processors.Add(processor);

            return this;
        }

        public virtual IHtmlGrid<T> Filterable(bool isFilterable)
        {
            foreach (IGridColumn<T> column in Grid.Columns)
                if (column.IsFilterable == null)
                    column.IsFilterable = isFilterable;

            return this;
        }
        public virtual IHtmlGrid<T> MultiFilterable()
        {
            foreach (IGridColumn<T> column in Grid.Columns)
                if (column.IsMultiFilterable == null)
                    column.IsMultiFilterable = true;

            return this;
        }
        public virtual IHtmlGrid<T> Filterable()
        {
            return Filterable(true);
        }

        public virtual IHtmlGrid<T> Sortable(bool isSortable)
        {
            foreach (IGridColumn<T> column in Grid.Columns)
                if (column.IsSortable == null)
                    column.IsSortable = isSortable;

            return this;
        }
        public virtual IHtmlGrid<T> Sortable()
        {
            return Sortable(true);
        }

        public virtual IHtmlGrid<T> RowCss(Func<T, string> cssClasses)
        {
            Grid.Rows.CssClasses = cssClasses;

            return this;
        }
        public virtual IHtmlGrid<T> Css(string cssClasses)
        {
            Grid.CssClasses = cssClasses;

            return this;
        }
        public virtual IHtmlGrid<T> Empty(string text)
        {
            Grid.EmptyText = text;

            return this;
        }
        public virtual IHtmlGrid<T> Named(string name)
        {
            Grid.Name = name;

            return this;
        }

        public virtual IHtmlGrid<T> Pageable(Action<IGridPager<T>> builder)
        {
            Grid.Pager = Grid.Pager ?? new GridPager<T>(Grid);
            builder(Grid.Pager);

            if (!Grid.Processors.Contains(Grid.Pager))
                Grid.Processors.Add(Grid.Pager);

            return this;
        }
        public virtual IHtmlGrid<T> Pageable()
        {
            return Pageable(builder => { });
        }

        public IHtmlGrid<T> IsDataTable(bool dataTable)
        {
            Grid.IsDataTable = dataTable;
            return this;
        }

        public IHtmlGrid<T> MultiSelectable(bool isMultiSelectable)
        {
            Grid.IsMultiSelectable = isMultiSelectable;
            return this;
        }

        public IHtmlGrid<T> Toolbar(string toolbar)
        {
            Grid.ToolbarId = toolbar;
            return this;
        }

        public IHtmlGrid<T> Ajax(string action)
        {
            Grid.Ajax = true;
            Grid.AjaxRoute = action;
            return this;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            Html.Partial(PartialViewName, Grid).WriteTo(writer, encoder);
        }
    }
}
