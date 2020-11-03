using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ITN.TS.DataGrid
{
    public class GridModel<T> where T : class
    {
        public GridModel(T parent, int? parentId, int id)
        {
            ParentId = parentId;
            Id = id;
            Parent = parent;
        }
        public GridModel(T parent, int? parentId, int id, ICollection<GridModel<T>> children)
        {
            Parent = parent;
            Children = children;
            ParentId = parentId;
            Id = id;
        }
        public int? ParentId { get; set; }
        public int Id { get; set; }

        public T Parent { get; set; }
        public ICollection<GridModel<T>> Children { get; set; }

    }
    public class GridTableModel
    {
        public object HtmlAttribute { get; set; }
        public List<GridColumn> Columns { get; set; }
        public List<GridTableRow> Rows { get; set; }

    }

    public class GridColumn
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class GridTreeModel
    {
        public GridTreeModel()
        {
            TreeNodes = new List<GridTreeNodes>();
        }
        public object HtmlAttribute { get; set; }
        public List<string> Columns { get; set; }
        public List<GridTreeNodes> TreeNodes { get; set; }

    }
    public class GridTreeNodes
    {
        public List<GridTreeRow> Rows { get; set; }
        public List<GridTreeNodes> ChildNodes { get; set; }

    }

    public class GridTreeRow
    {
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public bool IsFirstElementRow { get; set; }
        public string RowId { get; set; }
        public int ItemId { get; set; }
        public int? ParentId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }


    public class GridTableRow
    {
        public string ColumnName { get; set; }
        public object Value { get; set; }
        public string RouteValues { get; set; }
        public bool IsFirstElementRow { get; set; }
        public string RowId { get; set; }
        public int ItemId { get; set; }
        public string CellFormat { get; set; }

    }
    public class ColumnRow<T>
    {
        public string Html { get; set; }
        public Expression<Func<T, string>> DisplayFormat { get; set; }
        public Expression<Func<T, string>> RouteValues { get; set; }

        public Expression<Func<T, object>> Key { get; set; }


        public Expression<Func<T, object>> Column { get; set; }
        public Func<T, bool> Predicate;

        public bool Filtered(T obj)
        {
            return Predicate?.Invoke(obj) ?? true;
        }

        public string ColumnName =>
                        (Column.Body is MemberExpression
                            ? (MemberExpression)Column.Body
                            : ((MemberExpression)((UnaryExpression)Column.Body).Operand)).Member.Name;

        public string KeyColumn =>
                        (Key.Body as ConstantExpression)?.Value.ToString() ?? (Key.Body is MemberExpression
                            ? (MemberExpression)Key.Body
                            : ((MemberExpression)((UnaryExpression)Key.Body).Operand)).Member.Name;
    }
    public class GridProperty
    {
        public GridProperty()
        {
            DisplayAttribute = new List<Attribute>();
            Members = new List<string>();
        }
        public string FullName => string.Join(".", Members.Where(e => e != Members.First()))+"."+ DisplayName;
        public string Parameter { get; set; }
        public string DisplayName
            => DisplayAttribute.Any(e => e.GetType().IsAssignableFrom(typeof(DisplayAttribute)))
                ? ((DisplayAttribute)
                    DisplayAttribute.First(e => e.GetType().IsAssignableFrom(typeof(DisplayAttribute)))).Name
                : Parameter;
        public IEnumerable<string> Members { get; set; }
        public IEnumerable<Attribute> DisplayAttribute { get; set; }
    }
}
