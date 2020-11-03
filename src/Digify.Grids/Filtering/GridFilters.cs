using System;
using System.Collections.Generic;
using System.Linq;
using Digify.DataGrid.Columns;
using Digify.DataGrid.Filtering.Boolean;
using Digify.DataGrid.Filtering.Date;
using Digify.DataGrid.Filtering.Number;
using Digify.DataGrid.Filtering.Text;
using Microsoft.Extensions.Primitives;

namespace Digify.DataGrid.Filtering
{
    public class GridFilters : IGridFilters
    {
        public IDictionary<Type, IDictionary<string, Type>> Table
        {
            get;
        }

        public GridFilters()
        {
            Table = new Dictionary<Type, IDictionary<string, Type>>();

            Register(typeof(sbyte), "Equals", typeof(sbyteFilter));
            Register(typeof(sbyte), "NotEquals", typeof(sbyteFilter));
            Register(typeof(sbyte), "LessThan", typeof(sbyteFilter));
            Register(typeof(sbyte), "GreaterThan", typeof(sbyteFilter));
            Register(typeof(sbyte), "LessThanOrEqual", typeof(sbyteFilter));
            Register(typeof(sbyte), "GreaterThanOrEqual", typeof(sbyteFilter));

            Register(typeof(byte), "Equals", typeof(byteFilter));
            Register(typeof(byte), "NotEquals", typeof(byteFilter));
            Register(typeof(byte), "LessThan", typeof(byteFilter));
            Register(typeof(byte), "GreaterThan", typeof(byteFilter));
            Register(typeof(byte), "LessThanOrEqual", typeof(byteFilter));
            Register(typeof(byte), "GreaterThanOrEqual", typeof(byteFilter));

            Register(typeof(short), "Equals", typeof(shortFilter));
            Register(typeof(short), "NotEquals", typeof(shortFilter));
            Register(typeof(short), "LessThan", typeof(shortFilter));
            Register(typeof(short), "GreaterThan", typeof(shortFilter));
            Register(typeof(short), "LessThanOrEqual", typeof(shortFilter));
            Register(typeof(short), "GreaterThanOrEqual", typeof(shortFilter));

            Register(typeof(ushort), "Equals", typeof(ushortFilter));
            Register(typeof(ushort), "NotEquals", typeof(ushortFilter));
            Register(typeof(ushort), "LessThan", typeof(ushortFilter));
            Register(typeof(ushort), "GreaterThan", typeof(ushortFilter));
            Register(typeof(ushort), "LessThanOrEqual", typeof(ushortFilter));
            Register(typeof(ushort), "GreaterThanOrEqual", typeof(ushortFilter));

            Register(typeof(int), "Equals", typeof(intFilter));
            Register(typeof(int), "NotEquals", typeof(intFilter));
            Register(typeof(int), "LessThan", typeof(intFilter));
            Register(typeof(int), "GreaterThan", typeof(intFilter));
            Register(typeof(int), "LessThanOrEqual", typeof(intFilter));
            Register(typeof(int), "GreaterThanOrEqual", typeof(intFilter));

            Register(typeof(uint), "Equals", typeof(UintFilter));
            Register(typeof(uint), "NotEquals", typeof(UintFilter));
            Register(typeof(uint), "LessThan", typeof(UintFilter));
            Register(typeof(uint), "GreaterThan", typeof(UintFilter));
            Register(typeof(uint), "LessThanOrEqual", typeof(UintFilter));
            Register(typeof(uint), "GreaterThanOrEqual", typeof(UintFilter));

            Register(typeof(long), "Equals", typeof(Int64Filter));
            Register(typeof(long), "NotEquals", typeof(Int64Filter));
            Register(typeof(long), "LessThan", typeof(Int64Filter));
            Register(typeof(long), "GreaterThan", typeof(Int64Filter));
            Register(typeof(long), "LessThanOrEqual", typeof(Int64Filter));
            Register(typeof(long), "GreaterThanOrEqual", typeof(Int64Filter));

            Register(typeof(ulong), "Equals", typeof(UInt64Filter));
            Register(typeof(ulong), "NotEquals", typeof(UInt64Filter));
            Register(typeof(ulong), "LessThan", typeof(UInt64Filter));
            Register(typeof(ulong), "GreaterThan", typeof(UInt64Filter));
            Register(typeof(ulong), "LessThanOrEqual", typeof(UInt64Filter));
            Register(typeof(ulong), "GreaterThanOrEqual", typeof(UInt64Filter));

            Register(typeof(float), "Equals", typeof(SingleFilter));
            Register(typeof(float), "NotEquals", typeof(SingleFilter));
            Register(typeof(float), "LessThan", typeof(SingleFilter));
            Register(typeof(float), "GreaterThan", typeof(SingleFilter));
            Register(typeof(float), "LessThanOrEqual", typeof(SingleFilter));
            Register(typeof(float), "GreaterThanOrEqual", typeof(SingleFilter));

            Register(typeof(double), "Equals", typeof(doubleFilter));
            Register(typeof(double), "NotEquals", typeof(doubleFilter));
            Register(typeof(double), "LessThan", typeof(doubleFilter));
            Register(typeof(double), "GreaterThan", typeof(doubleFilter));
            Register(typeof(double), "LessThanOrEqual", typeof(doubleFilter));
            Register(typeof(double), "GreaterThanOrEqual", typeof(doubleFilter));

            Register(typeof(Decimal), "Equals", typeof(DecimalFilter));
            Register(typeof(Decimal), "NotEquals", typeof(DecimalFilter));
            Register(typeof(Decimal), "LessThan", typeof(DecimalFilter));
            Register(typeof(Decimal), "GreaterThan", typeof(DecimalFilter));
            Register(typeof(Decimal), "LessThanOrEqual", typeof(DecimalFilter));
            Register(typeof(Decimal), "GreaterThanOrEqual", typeof(DecimalFilter));

            Register(typeof(DateTime), "Equals", typeof(DateTimeFilter));
            Register(typeof(DateTime), "NotEquals", typeof(DateTimeFilter));
            Register(typeof(DateTime), "LessThan", typeof(DateTimeFilter));
            Register(typeof(DateTime), "GreaterThan", typeof(DateTimeFilter));
            Register(typeof(DateTime), "LessThanOrEqual", typeof(DateTimeFilter));
            Register(typeof(DateTime), "GreaterThanOrEqual", typeof(DateTimeFilter));

            Register(typeof(bool), "Equals", typeof(boolFilter));

            Register(typeof(string), "Equals", typeof(stringEqualsFilter));
            Register(typeof(string), "NotEquals", typeof(stringNotEqualsFilter));
            Register(typeof(string), "Contains", typeof(stringContainsFilter));
            Register(typeof(string), "EndsWith", typeof(stringEndsWithFilter));
            Register(typeof(string), "StartsWith", typeof(stringStartsWithFilter));
        }

        public IGridColumnFilter<T> GetFilter<T>(IGridColumn<T> column)
        {
            string[] keys = column
                .Grid
                .Query
                .Keys
                .Where(key =>
                    (key ?? "").StartsWith(column.Grid.Name + "-" + column.Name + "-") &&
                    key != column.Grid.Name + "-" + column.Name + "-Op")
                .ToArray();

            GridColumnFilter<T> filter = new GridColumnFilter<T>();
            filter.Second = GetSecondFilter(column, keys);
            filter.First = GetFirstFilter(column, keys);
            filter.Operator = GetOperator(column);
            filter.Column = column;

            return filter;
        }

        public void Register(Type forType, string filterType, Type filter)
        {
            IDictionary<string, Type> typedFilters = new Dictionary<string, Type>();
            Type underlyingType = Nullable.GetUnderlyingType(forType) ?? forType;

            if (Table.ContainsKey(underlyingType))
                typedFilters = Table[underlyingType];
            else
                Table.Add(underlyingType, typedFilters);

            typedFilters[filterType] = filter;
        }
        public void Unregister(Type forType, string filterType)
        {
            if (Table.ContainsKey(forType))
                Table[forType].Remove(filterType);
        }

        private IGridFilter GetFilter<T>(IGridColumn<T> column, string type, string value)
        {
            Type valueType = Nullable.GetUnderlyingType(column.Expression.ReturnType) ?? column.Expression.ReturnType;
            if (!Table.ContainsKey(valueType))
                return null;

            IDictionary<string, Type> typedFilters = Table[valueType];
            if (!typedFilters.ContainsKey(type))
                return null;

            IGridFilter filter = (IGridFilter)Activator.CreateInstance(typedFilters[type]);
            filter.Value = value;
            filter.Type = type;

            return filter;
        }
        private IGridFilter GetSecondFilter<T>(IGridColumn<T> column, string[] keys)
        {
            if (column.IsMultiFilterable != true || keys.Length == 0)
                return null;

            if (keys.Length == 1)
            {
                StringValues values = column.Grid.Query[keys[0]];
                if (values.Count < 2) return null;

                string keyType = keys[0].Substring((column.Grid.Name + "-" + column.Name + "-").Length);

                return GetFilter(column, keyType, values[1]);
            }

            string type = keys[1].Substring((column.Grid.Name + "-" + column.Name + "-").Length);
            string value = column.Grid.Query[keys[1]][0];

            return GetFilter(column, type, value);
        }
        private IGridFilter GetFirstFilter<T>(IGridColumn<T> column, string[] keys)
        {
            if (keys.Length == 0) return null;

            string type = keys[0].Substring((column.Grid.Name + "-" + column.Name + "-").Length);
            string value = column.Grid.Query[keys[0]][0];

            return GetFilter(column, type, value);
        }
        private string GetOperator<T>(IGridColumn<T> column)
        {
            StringValues values = column.Grid.Query[column.Grid.Name + "-" + column.Name + "-Op"];
            if (column.IsMultiFilterable != true) return null;

            return values.FirstOrDefault();
        }
    }
}
