using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using Digify.DataGrid.Filtering;
using Digify.DataGrid.Grids;
using Digify.DataGrid.Processors;
using Digify.DataGrid.Rows;
using Digify.DataGrid.Sorting;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;

namespace Digify.DataGrid.Columns
{
    public class GridColumn<T, TValue> : BaseGridColumn<T, TValue> where T : class
    {
        private bool SortOrderIsSet { get; set; }
        public override GridSortOrder? SortOrder
        {
            get
            {
                if (SortOrderIsSet)
                    return base.SortOrder;

                if (Grid.Query[Grid.Name + "-Sort"] == Name)
                {
                    string orderValue = Grid.Query[Grid.Name + "-Order"];
                    GridSortOrder order;

                    if (Enum.TryParse(orderValue, out order))
                        SortOrder = order;
                }
                else if (Grid.Query[Grid.Name + "-Sort"] == StringValues.Empty)
                {
                    SortOrder = InitialSortOrder;
                }

                SortOrderIsSet = true;

                return base.SortOrder;
            }
            set
            {
                base.SortOrder = value;
                SortOrderIsSet = true;
            }
        }

        private bool FilterIsSet { get; set; }
        public override IGridColumnFilter<T> Filter
        {
            get
            {
                if (!FilterIsSet)
                    Filter = Grid.ViewContext.HttpContext.RequestServices.GetRequiredService<IGridFilters>().GetFilter(this);

                return base.Filter;
            }
            set
            {
                base.Filter = value;
                FilterIsSet = true;
            }
        }

        public GridColumn(IGrid<T> grid, Expression<Func<T, TValue>> expression)
        {
            Grid = grid;
            IsEncoded = false;
            Expression = expression;
            Title = GetTitle(expression);
            FilterName = GetFilterName();
            ProcessorType = GridProcessorType.Pre;
            ExpressionValue = expression.Compile();
            IsSortable = IsFilterable = IsMember(expression);
            Name = GetName(expression);
        }
        public GridColumn(IGrid<T> grid, string name)
        {
            Grid = grid;
            IsEncoded = false;
            Title =new HtmlString(name);
            FilterName = GetFilterName();
            ProcessorType = GridProcessorType.Pre;
            Name = name;
        }
        public override IQueryable<T> Process(IQueryable<T> items)
        {
            if (IsFilterable == true && Filter != null)
                items = Filter.Process(items);

            if (IsSortable != true || SortOrder == null)
                return items;

            if (SortOrder == GridSortOrder.Asc)
                return items.OrderBy(Expression);

            return items.OrderByDescending(Expression);
        }
        public override IHtmlContent ValueFor(IGridRow<object> row)
        {
            object value = GetValueFor(row);
            if (value == null) return HtmlString.Empty;
            if (value is IHtmlContent) return value as IHtmlContent;
            if (FormatExpression != null)
            {
                Format = FormatExpression.Invoke((T)row.Model);
            }
            if (RouteValues != null)
            {
                Format = $"<a href=\"{RouteValues.Invoke((T)row.Model)}\">{Format}</a>";
            }
            if (Format != null) value = string.Format(Format, value);
            if (IsEncoded) return new HtmlString(HtmlEncoder.Default.Encode(value.ToString()));

            return new HtmlString(value.ToString());
        }
        public override IHtmlContent ValueForKey(IGridRow<object> row)
        {
            object value = GetValueFor(row);
            if (value == null) return HtmlString.Empty;
            if (value is IHtmlContent) return value as IHtmlContent;
            if (IsEncoded) return new HtmlString(HtmlEncoder.Default.Encode(value.ToString()));

            return new HtmlString(value.ToString());
        }
        private bool? IsMember(Expression<Func<T, TValue>> expression)
        {
            if (expression.Body is MemberExpression)
                return null;

            return false;
        }

        private IHtmlContent GetTitle(Expression<Func<T, TValue>> expression)
        {
            MemberExpression body = expression.Body as MemberExpression;
            DisplayAttribute display = body?.Member.GetCustomAttribute<DisplayAttribute>();

            return new HtmlString(display?.GetShortName() ?? GetName(expression));
        }
        private object GetValueFor(IGridRow<object> row)
        {
            try
            {
                if (RenderValue != null)
                    return RenderValue(row.Model as T);
                if(ExpressionValue != null)
                return ExpressionValue(row.Model as T);

                return row.Model.GetType().GetProperty(Name).GetValue(row.Model, null);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        private string GetFilterName()
        {
            Type type = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
            if (type.GetTypeInfo().IsEnum) return null;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return "Number";
                case TypeCode.String:
                    return "Text";
                case TypeCode.DateTime:
                    return "Date";
                case TypeCode.Boolean:
                    return "bool";
                default:
                    return null;
            }
        }
        public string GetName(Expression<Func<T, TValue>> exp)
        {
            MemberExpression body = exp.Body as MemberExpression;
            ConstantExpression content = exp.Body as ConstantExpression;

            if (content != null)
            {
                return content.Value.ToString();
            }else if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }
    }
}
