using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Digify.DataGrid.Filtering;

namespace Digify.DataGrid.ModelBindings
{
    public class GridModelBinder : IModelBinder
    {
        private readonly CamelCaseRequestNameConvention _requestConvention;
        public Func<ModelBindingContext, IDictionary<string, object>> ParseAdditionalParameters;

        public GridModelBinder()
        {
            _requestConvention = new CamelCaseRequestNameConvention();
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            return Task.Factory.StartNew(() =>
            {
                BindModel(
                    bindingContext,
                    ParseAdditionalParameters);
            });
        }


        public virtual void BindModel(ModelBindingContext bindingContext, Func<ModelBindingContext, IDictionary<string, object>> parseAditionalParameters)
        {
            if (!bindingContext.ModelType.IsAssignableFrom(typeof(DataTableRequest)))
            {
                return;
            }

            var values = bindingContext.ValueProvider;

            var draw = values.GetValue(_requestConvention.Draw);
            int _draw = 0;
            Parse<int>(draw, out _draw);
            var start = values.GetValue(_requestConvention.Start);
            int _start = 0;
            Parse<int>(start, out _start);

            var length = values.GetValue(_requestConvention.Length);
            int _length = 10;
            Parse<int>(length, out _length);

            var searchValue = values.GetValue(_requestConvention.SearchValue);
            string _searchValue = null;
            Parse<string>(searchValue, out _searchValue);

            var searchRegex = values.GetValue(_requestConvention.IsSearchRegex);
            bool _searchRegex = false;
            Parse<bool>(searchRegex, out _searchRegex);

            var search = new SearchQuery()
            {
                Value = _searchValue,
                IsRegex = _searchRegex
            };

            // Parse columns & column sorting.
            var columns = ParseColumns(values, _requestConvention).ToList();
            var sorting = ParseSorting(columns, values, _requestConvention);

            if (parseAditionalParameters != null)
            {
                var aditionalParameters = parseAditionalParameters(bindingContext);
                var model = new DataTableRequest()
                {
                    SearchQuery = search,
                    Columns = columns,
                    Start = _start,
                    Draw = _draw,
                    Length = _length,
                    AditionalParameters = aditionalParameters
                };
                {
                    //return ModelBindingResult.Success(bindingContext.ModelName, model);
                    bindingContext.Result = ModelBindingResult.Success(model);
                    return;
                }
            }
            else
            {
                var model = new DataTableRequest()
                {
                    SearchQuery = search,
                    Columns = columns,
                    Start = _start,
                    Draw = _draw,
                    Length = _length,
                    AditionalParameters = null
                };
                {
                    //return ModelBindingResult.Success(bindingContext.ModelName, model);
                    bindingContext.Result = ModelBindingResult.Success(model);
                    return;
                }
            }
        }



        private static IEnumerable<Column> ParseColumns(IValueProvider values, CamelCaseRequestNameConvention names)
        {
            var columns = new List<Column>();

            int counter = 1;
            while (true)
            {
                // Parses Field value.
                var columnField = values.GetValue(String.Format(names.ColumnField, counter));
                string _columnField = null;
                if (!Parse(columnField, out _columnField)) break;

                // Parses Name value.
                var columnName = values.GetValue(String.Format(names.ColumnName, counter));
                string _columnName = null;
                if (!Parse(columnName, out _columnName)) break;

                // Parses Orderable value.
                var columnSortable = values.GetValue(String.Format(names.IsColumnSortable, counter));
                bool _columnSortable = true;
                Parse(columnSortable, out _columnSortable);

                // Parses Searchable value.
                var columnSearchable = values.GetValue(String.Format(names.IsColumnSearchable, counter));
                var _columnSearchable = true;
                Parse(columnSearchable, out _columnSearchable);

                // Parsed SearchQuery value.
                var columnSearchValue = values.GetValue(String.Format(names.ColumnSearchValue, counter));
                string _columnSearchValue = null;
                Parse(columnSearchValue, out _columnSearchValue);

                // Parses IsRegex value.
                var columnSearchRegex = values.GetValue(String.Format(names.IsColumnSearchRegex, counter));
                bool _columnSearchRegex = false;
                Parse(columnSearchRegex, out _columnSearchRegex);

                var search = new SearchQuery()
                {
                    Value = _columnSearchValue,
                    IsRegex = _columnSearchRegex
                };

                var column = new Column()
                {
                    ColumnField = _columnField,
                    Name = _columnName,
                    IsSearchable = _columnSearchable,
                    IsSortable = _columnSortable,
                    SearchQuery = search
                };

                columns.Add(column);

                counter++;
            }

            return columns;
        }

        private static IEnumerable<Sort> ParseSorting(IEnumerable<Column> columns, IValueProvider values, CamelCaseRequestNameConvention names)
        {
            var sorting = new List<Sort>();

            for (int i = 0; i < columns.Count(); i++)
            {
                var sortField = values.GetValue(String.Format(names.SortField, i));
                int _sortField = 0;
                if (!Parse<int>(sortField, out _sortField)) break;

                var column = columns.ElementAt(_sortField);

                var sortDirection = values.GetValue(String.Format(names.SortDirection, i));
                string _sortDirection = null;
                Parse<string>(sortDirection, out _sortDirection);

                if (column.SetSort(i, _sortDirection))
                    sorting.Add(column.Sort);
            }

            return sorting;
        }

        private static bool Parse<ElementType>(ValueProviderResult value, out ElementType result)
        {
            result = default(ElementType);

            if (value == null) return false;
            if (string.IsNullOrWhiteSpace(value.FirstValue)) return false;

            try
            {
                result = (ElementType)Convert.ChangeType(value.FirstValue, typeof(ElementType));
                return true;
            }
            catch { return false; }
        }
    }
}
